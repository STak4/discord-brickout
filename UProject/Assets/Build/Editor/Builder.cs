using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace STak4
{
    /// <summary>
    /// https://gitlab.com/game-ci/unity3d-gitlab-ci-example/-/blob/main/Assets/Scripts/Editor/BuildCommand.cs
    /// </summary>
    public static class Builder
    {
        private const string BUILD_OPTIONS_ENV_VAR = "BuildOptions";
        private const string SCRIPTING_BACKEND_ENV_VAR = "SCRIPTING_BACKEND";
        private const string VERSION_NUMBER_VAR = "VERSION_NUMBER_VAR";

        [MenuItem("Build/Build")]
        public static void Build()
        {
            var buildTarget = GetBuildTarget();
            if (buildTarget == BuildTarget.NoTarget) buildTarget = EditorUserBuildSettings.activeBuildTarget;

            Console.WriteLine(":: Performing build");
            if (TryGetEnv(VERSION_NUMBER_VAR, out var bundleVersionNumber))
            {
                Console.WriteLine(
                    $":: Setting bundleVersionNumber to '{bundleVersionNumber}' (Length: {bundleVersionNumber.Length})");
                PlayerSettings.bundleVersion = bundleVersionNumber;
            }

            var buildPath = GetBuildPath();
            var buildName = GetBuildName();
            var buildOptions = GetBuildOptions();
            var fixedBuildPath = GetFixedBuildPath(buildTarget, buildPath, buildName);

            SetScriptingBackendFromEnv(buildTarget);

            if (string.IsNullOrEmpty(fixedBuildPath))
            {
                fixedBuildPath = EditorUtility.SaveFolderPanel("Choose Location", "", "");
                //パスが入っていれば選択されたとみなす（キャンセルされたら入ってこない）
                if (string.IsNullOrEmpty(fixedBuildPath))
                {
                    return;
                }
            }

            var buildReport = BuildPipeline.BuildPlayer(GetEnabledScenes(), fixedBuildPath, buildTarget, buildOptions);

            if (buildReport.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
                throw new Exception($"Build ended with {buildReport.summary.result} status");

            Console.WriteLine(":: Done with build");

            EditorApplication.Exit(buildReport.summary.result == BuildResult.Succeeded ? 0 : 1);
        }

        static string GetArgument(string name)
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains(name))
                {
                    return args[i + 1];
                }
            }

            return null;
        }

        static string[] GetEnabledScenes()
        {
            return (
                from scene in EditorBuildSettings.scenes
                where scene.enabled
                where !string.IsNullOrEmpty(scene.path)
                select scene.path
            ).ToArray();
        }

        static BuildTarget GetBuildTarget()
        {
            string buildTargetName = GetArgument("customBuildTarget");
            Console.WriteLine(":: Received customBuildTarget " + buildTargetName);

            if (buildTargetName.TryConvertToEnum(out BuildTarget target))
                return target;

            Console.WriteLine(
                $":: {nameof(buildTargetName)} \"{buildTargetName}\" not defined on enum {nameof(BuildTarget)}, using {nameof(BuildTarget.NoTarget)} enum to build");

            return BuildTarget.NoTarget;
        }

        static string GetBuildPath()
        {
            string buildPath = GetArgument("customBuildPath");
            Console.WriteLine(":: Received customBuildPath " + buildPath);
            if (buildPath == "")
            {
                throw new Exception("customBuildPath argument is missing");
            }

            return buildPath;
        }

        static string GetBuildName()
        {
            string buildName = GetArgument("customBuildName");
            Console.WriteLine(":: Received customBuildName " + buildName);
            if (buildName == "")
            {
                throw new Exception("customBuildName argument is missing");
            }

            return buildName;
        }

        static string GetFixedBuildPath(BuildTarget buildTarget, string buildPath, string buildName)
        {
            if (buildTarget.ToString().ToLower().Contains("windows"))
            {
                buildName += ".exe";
            }
            else if (buildTarget == BuildTarget.Android)
            {
#if UNITY_2018_3_OR_NEWER
                buildName += EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk";
#else
            buildName += ".apk";
#endif
            }

            return buildPath + buildName;
        }

        static BuildOptions GetBuildOptions()
        {
            if (TryGetEnv(BUILD_OPTIONS_ENV_VAR, out string envVar))
            {
                string[] allOptionVars = envVar.Split(',');
                BuildOptions allOptions = BuildOptions.None;
                BuildOptions option;
                string optionVar;
                int length = allOptionVars.Length;

                Console.WriteLine($":: Detecting {BUILD_OPTIONS_ENV_VAR} env var with {length} elements ({envVar})");

                for (int i = 0; i < length; i++)
                {
                    optionVar = allOptionVars[i];

                    if (optionVar.TryConvertToEnum(out option))
                    {
                        allOptions |= option;
                    }
                    else
                    {
                        Console.WriteLine(
                            $":: Cannot convert {optionVar} to {nameof(BuildOptions)} enum, skipping it.");
                    }
                }

                return allOptions;
            }

            return BuildOptions.None;
        }


        static bool TryGetEnv(string key, out string value)
        {
            value = Environment.GetEnvironmentVariable(key);
            return !string.IsNullOrEmpty(value);
        }

        // https://stackoverflow.com/questions/1082532/how-to-tryparse-for-enum-value
        static bool TryConvertToEnum<TEnum>(this string strEnumValue, out TEnum value)
        {
            if (string.IsNullOrEmpty(strEnumValue) || !Enum.IsDefined(typeof(TEnum), strEnumValue))
            {
                value = default;
                return false;
            }

            value = (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
            return true;
        }

        static void SetScriptingBackendFromEnv(BuildTarget platform)
        {
            var targetGroup = BuildPipeline.GetBuildTargetGroup(platform);
            if (TryGetEnv(SCRIPTING_BACKEND_ENV_VAR, out string scriptingBackend))
            {
                if (scriptingBackend.TryConvertToEnum(out ScriptingImplementation backend))
                {
                    Console.WriteLine($":: Setting ScriptingBackend to {backend}");
                    PlayerSettings.SetScriptingBackend(NamedBuildTarget.FromBuildTargetGroup(targetGroup), backend);
                }
                else
                {
                    string possibleValues = string.Join(", ",
                        Enum.GetValues(typeof(ScriptingImplementation)).Cast<ScriptingImplementation>());
                    throw new Exception(
                        $"Could not find '{scriptingBackend}' in ScriptingImplementation enum. Possible values are: {possibleValues}");
                }
            }
            else
            {
                var defaultBackend =
                    PlayerSettings.GetDefaultScriptingBackend(NamedBuildTarget.FromBuildTargetGroup(targetGroup));
                Console.WriteLine(
                    $":: Using project's configured ScriptingBackend (should be {defaultBackend} for targetGroup {targetGroup}");
            }
        }
    }
}