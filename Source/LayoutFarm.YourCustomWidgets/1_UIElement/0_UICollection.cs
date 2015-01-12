﻿// 2015,2014 ,Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PixelFarm.Drawing;


namespace LayoutFarm.UI
{

    public class UICollection
    {

        List<UIElement> uiList = new List<UIElement>();
        UIElement owner;
        public UICollection(UIElement owner)
        {
            this.owner = owner;
        }
        public void AddUI(UIElement ui)
        {
#if DEBUG
            if (this.owner == ui)
                throw new Exception("cyclic!");
#endif
            ui.ParentUI = this.owner;
            uiList.Add(ui);
        }
        public int Count
        {
            get { return this.uiList.Count; }
        }
        public void RemoveUI(UIElement ui)
        {
            //remove specific ui
        }
        public void RemoveAt(int index)
        {
            uiList.RemoveAt(index);
        }
        public void Clear()
        {
            this.uiList.Clear();
        }
        public UIElement GetElement(int index)
        {
            return this.uiList[index];
        }


    }


}