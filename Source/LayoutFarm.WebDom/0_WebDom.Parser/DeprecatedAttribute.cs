﻿//BSD, 2014-2018, WinterDev

using System;
namespace LayoutFarm
{
    public class FeatureDeprecatedAttribute : Attribute
    {
        public FeatureDeprecatedAttribute()
        {
        }
        public FeatureDeprecatedAttribute(string note)
        {
            this.Note = note;
        }
        public string Note { get; set; }
    }
}