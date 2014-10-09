﻿//2014 Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;
using LayoutFarm.SampleControls;
using LayoutFarm.UI;

namespace LayoutFarm
{
    [DemoNote("4.3.2 UIHtmlBox with ContentMx")]
    class Demo_UIHtmlBox_ContentMx : DemoBase
    {

        string imgFolderPath = null;
        protected override void OnStartDemo(UISurfaceViewportControl viewport)
        {
            var appPath = System.Windows.Forms.Application.ExecutablePath;
            int pos = appPath.IndexOf("\\bin\\");
            if (pos > -1)
            {
                string sub01 = appPath.Substring(0, pos);
                imgFolderPath = sub01 + "\\images";

            }
            //==================================================
            //html box
            var htmlBox = new UIHtmlBox(800, 600);
            var htmlBoxContentMx = new UIHtmlBoxContentManager();
            var contentMx = new HtmlRenderer.ContentManagers.ImageContentManager();

                   
            htmlBoxContentMx.AddImageContentMan(contentMx);
            htmlBoxContentMx.Bind(htmlBox);    


            contentMx.ImageLoadingRequest += new EventHandler<HtmlRenderer.ContentManagers.ImageRequestEventArgs>(contentMx_ImageLoadingRequest);

             
            viewport.AddContent(htmlBox);
            string html = "<html><head></head><body><div>OK1</div><div>3 Images</div><img src=\"sample01.png\"></img><img src=\"sample01.png\"></img><img src=\"sample01.png\"></img></body></html>";
            htmlBox.LoadHtmlText(html);
        }

        void contentMx_ImageLoadingRequest(object sender, HtmlRenderer.ContentManagers.ImageRequestEventArgs e)
        {
            //load resource -- sync or async? 
            string absolutePath = imgFolderPath + "\\" + e.ImagSource;
            if (!System.IO.File.Exists(absolutePath))
            {
                return;
            } 
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(absolutePath);
            e.SetResultImage(CurrentGraphicPlatform.P.CreateBitmap(bmp));
        }
        
    }
}