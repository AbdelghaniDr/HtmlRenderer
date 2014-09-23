﻿//from github.com/vvvv/svg 
//license : Microsoft Public License (MS-PL) 

﻿using System;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;
using System.Globalization;
using HtmlRenderer;

namespace Svg.Transforms
{
    /// <summary>
    /// The class which applies custom transform to this Matrix (Required for projects created by the Inkscape).
    /// </summary>
    public sealed class SvgMatrix : SvgTransform
    {
        private List<float> points;

        public List<float> Points
        {
            get { return this.points; }
            set { this.points = value; }
        }

        public override Matrix Matrix
        {
            get
            {
                Matrix matrix = CurrentGraphicPlatform.P.CreateMatrix(
                    this.points[0],
                    this.points[1],
                    this.points[2],
                    this.points[3],
                    this.points[4],
                    this.points[5]
                );
                return matrix;
            }
        }

        public override string WriteToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "matrix({0}, {1}, {2}, {3}, {4}, {5})",
                this.points[0], this.points[1], this.points[2], this.points[3], this.points[4], this.points[5]);
        }

        public SvgMatrix(List<float> m)
        {
            this.points = m;
        }


        public override object Clone()
        {
            return new SvgMatrix(this.Points);
        }

    }
}