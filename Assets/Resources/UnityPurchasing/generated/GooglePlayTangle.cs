#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security
{
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("mhkXGCiaGRIamhkZGIUg/FDY2LMm3w4QXAfkZ53+1Uu0OyZ3HwILvlWc4Nmp7yHL7EJLDWQzCOINxXOpMchesaVqV2qErDUwPAN8FrZmoMgomhk6KBUeETKeUJ7vFRkZGR0YG3OQy+mIxsZl6oS4W0+zVUHhKOB0qc0vtsqr5lxnZ2yeq7Yr55z0k4OBULO7u4ThMAypCBck682Dib09ximkQoTyH/82ygcSLtGtrgOwxzb4INNUwk3b/hFEQKVQkAeBJPKF4lo9SMtJGjOzE1ApIPezmvoXI3lyEQ3Ni8VZfMe87iqW92ZAbYedwpuMqCfLihF4kkJwvkjyn6FS0LWzyYCjm1bVuAkrf9otGV5ktWksmd9P5P6RuvImy7K4pxobGRgZ");
        private static int[] order = new int[] { 1, 4, 7, 11, 4, 7, 11, 12, 8, 10, 10, 11, 13, 13, 14 };
        private static int key = 24;

        public static readonly bool IsPopulated = true;

        public static byte[] Data()
        {
            if (IsPopulated == false)
                return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
