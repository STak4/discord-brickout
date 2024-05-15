import { useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

interface UnityComponentProps {
  userName: string;
}

const UnityComponent = (props: UnityComponentProps) => {
  // UnityContextを準備、表示するUniyアプリを指定
  const { unityProvider, sendMessage, isLoaded } = useUnityContext({
    loaderUrl: "Build/Builds.loader.js",
    dataUrl: "Build/Builds.data",
    frameworkUrl: "Build/Builds.framework.js",
    codeUrl: "Build/Builds.wasm",
  });

  // useEffectの対象にisLoadedを含めない場合
  // 環境によってはsendMessageが動作しない問題がある
  useEffect(() => {
    if (isLoaded) {
      // Unityアプリに対してメッセージを送信
      // sendMessage("オブジェクト名", "関数名", 引数)
      sendMessage("EntryPoint", "ReceiveMessage", props.userName);
      console.log(`[Debug]SendMessage:${props.userName}`);
    }
  }, [isLoaded]);

  return <Unity id="unity-canvas" unityProvider={unityProvider} />;
};

export default UnityComponent;