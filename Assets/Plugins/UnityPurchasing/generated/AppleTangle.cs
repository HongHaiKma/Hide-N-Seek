#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("4/7x/vT24/K39e639vnut+f25eORlMKKmZOBk4O8R/7QA+GeaWP8GujWPw9uRl3xC7P8hkc0LHOMvVSImpGevRHfEWCalpaSkpeUFZaWl8smp897zZOlG/8kGIpJ8uRo8MnyK8Xy+/72+fTyt/j5t+P//uS39PLlpxWTLKcVlDQ3lJWWlZWWlaeakZ778rfe+fS5prGns5GUwpOchIrW595P4Qikg/I24ANeupWUlpeWNBWWk5GElcLEpoSnhpGUwpOdhJ3W5+efvJGWkpKQlZaBif/j4+fkrbi44P7x/vT24/74+bfW4uP/+OX+4+6moQ7buu8gehsMS2TgDGXhReCn2Faxp7ORlMKTnISK1ufn+/K31PLl4xWWl5GevRHfEWD085KWpxZlp72RF4O8R/7QA+GeaWP8GrnXMWDQ2ugirTpjmJmXBZwmtoG540Krmkz1gRjkFvdRjMyeuAUlb9PfZ/evCYJiTqHoVhDCTjAOLqXVbE9C5gnpNsWipaajp6ShzYCapKKnpaeupaajp+f78rfF+Pjjt9TWp4mAmqehp6Ol9fvyt+Tj9vnz9uXzt+Py5frkt/akoc2n9aacp56RlMKTkYSVwsSmhLf2+fO39PLl4/7x/vT24/74+bfnu7f08uXj/vH+9Pbj8rfn+Pv+9O7S6Yjb/McB1h5T4/WchxTWEKQdFjw05gXQxMJWOLjWJG9sdOdacTTb4//45f7j7qaBp4ORlMKTlISa1ufl9vTj/vTyt+Tj9uPy+vL54+S5pz9L6bWiXbJCTphB/EM1s7SGYDY7iBIUEowOqtCgZT4M1xm7QyYHhU/OMJKe64DXwYaJ40QgHLSs0DRC+Clj5Ax5RfOYXO7Yo081qW7vaPxfAgntmzPQHMxDgaCkXFOY2lmD/kbzorSC3ILOiiQDYGELCVjHLVbPx70R3xFgmpaWkpKXp/WmnKeekZTCuKcWVJGfvJGWkpKQlZWnFiGNFiRejuViyplC6MgMZbKULcIY2sqaZpGnmJGUwoqElpZok5KnlJaWaKeK8BifI7dgXDu7t/jnIaiWpxsg1FinhpGUwpOdhJ3W5+f78rfe+fS5prf48bfj//K34//y+bf25+f7/vT24OC59ufn+/K59Pj6uPbn5/vy9Pbut/bk5OL68uS39vT08ufj9vn08ogGTInQx3ySesnuE7p8oTXA28J7mAqqZLzev41faVkiLplOyYtBXKogjCoE1bOFvVCYiiHaC8n0X9wXgOf78rfU8uXj/vH+9Pbj/vj5t9bit9TWpxWWtaeakZ69Ed8RYJqWlpaSl5QVlpiXpxWWnZUVlpaXcwY+npB76q4UHMS3RK9TJigN2J38aLxrqrHwtx2k/WCaFVhJfDS4bsT9zPP587f0+Pnz/uP++Pnkt/jxt+Lk8hyOHklu3PtikDy1p5V/j6lvx55Es3V8RiDnSJjSdrBdZvrvenAigIC51zFg0Nron8mniJGUwoq0k4+ngVf0pOBgrZC7wXxNmLaZTS3kjtgin8mnFZaGkZTCireTFZafpxWWk6eBp4ORlMKTlISa1ufn+/K3xfj44+2nFZbhp5mRlMKKmJaWaJOTlJWWxz0dQk1za0eekKAn4uK2");
        private static int[] order = new int[] { 39,30,17,47,26,38,39,47,27,17,35,11,48,35,34,32,23,55,58,47,39,42,27,45,44,25,44,38,46,54,49,52,50,58,39,49,37,47,54,42,55,41,47,55,50,54,59,50,49,50,51,56,55,54,54,55,57,58,58,59,60 };
        private static int key = 151;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
