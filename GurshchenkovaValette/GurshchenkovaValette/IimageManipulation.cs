using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GurshchenkovaValette
{
    interface IimageManipulation
    {

        // save the image at the selected folder with the selected name
        Boolean save(Image img);

        // removes the selected image
        Boolean remove(String fileName);

        // open the selected image from the selected folder
        Image openImage(String fileName);
    }
}
