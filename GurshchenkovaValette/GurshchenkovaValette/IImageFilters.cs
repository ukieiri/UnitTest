using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurshchenkovaValette
{
    public interface IImageFilters
    {
        //apply color filter at your own taste
        Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green);

        //black and white filter
        Bitmap BlackWhite(Bitmap Bmp);

        //apply color filter to swap pixel colors
        Bitmap ApplyFilterSwap(Bitmap bmp);

    }
}
