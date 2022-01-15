using Demo.Common;
using Demo.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Demo.Services
{
    public static class AvatarProvider
    {
        private static readonly Dictionary<int, ImageSource> sourcesMap;

        static AvatarProvider()
        {
            sourcesMap = new Dictionary<int, ImageSource>
            {
                { 1, ImageUtils.ConvertToImageSource(Resources.Avatar_1) },
                { 2, ImageUtils.ConvertToImageSource(Resources.Avatar_2) },
                { 3, ImageUtils.ConvertToImageSource(Resources.Avatar_3) },
                { 4, ImageUtils.ConvertToImageSource(Resources.Avatar_4) },
                { 5, ImageUtils.ConvertToImageSource(Resources.Avatar_5) },
                { 6, ImageUtils.ConvertToImageSource(Resources.Avatar_6) },
                { 7, ImageUtils.ConvertToImageSource(Resources.Avatar_7) },
                { 8, ImageUtils.ConvertToImageSource(Resources.Avatar_8) },
            };
        }

        public static ImageSource GetAvatarSource(int avatarId)
        {
            if (sourcesMap != null && sourcesMap.ContainsKey(avatarId))
            {
                return sourcesMap[avatarId];
            }
            return null;
        }

        public static IEnumerable<int> GetAllIds()
        {
            return sourcesMap.Keys;
        }
    }
}
