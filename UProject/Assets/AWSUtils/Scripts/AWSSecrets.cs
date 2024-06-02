using Amazon;

namespace AWSUtils
{
    public class AwsSecrets
    {
        public static class Auth
        {
            public static readonly string AccessKey = "YOUR_ACCESS_KEY";
            public static readonly string SecretKey = "YOUR_SECRET_KEY";
        }

        public static class Bucket
        {
            public static readonly string BucketName = "YOUR_BACKET_NAME";
            public static readonly RegionEndpoint Region = RegionEndpoint.APNortheast1;
        }
        
        public static class CloudFront
        {
            public static readonly string DistrubutionId = "YOUR_DISTRIBUTION_ID";
        }   
    }
}