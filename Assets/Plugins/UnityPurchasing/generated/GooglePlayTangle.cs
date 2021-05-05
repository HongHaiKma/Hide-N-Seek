#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("/YIUxN9CmXv+PBwdYpHcQczdhHB2mWSUDAX7ESoJw0fF9wxZgAOGQlwm9HxpB3ObbWtGQwURm+OE967LIgNrbtVbZf5nTSn1E01BbOmayF56/T1B6s5OAbAb6B7kG3+VJBnUQ6WeCDq58qQEB1lrj3L0bVUi7yoxFgGvtMGCG0hdCY+onBcaBOAvoJFLyMbJ+UvIw8tLyMjJXg3qeFr5RvZf58QntrB1O/qEIYCaxd/W3/F76H6tsOx2f754CiHz98ShxhIrWhgrn4AXmixS1IopK8AeJikBRg1Bq6iVwKCARlPXLogoKh6Ylo2wkx7R6s3OwLgYA9rLoZ9b6mVCM/i8rdv5S8jr+cTPwONPgU8+xMjIyMzJygKLEABZ2vv5qsvKyMnI");
        private static int[] order = new int[] { 10,3,12,11,8,9,7,11,12,9,12,11,13,13,14 };
        private static int key = 201;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
