//from github.com/vvvv/svg 
//license : Microsoft Public License (MS-PL) 

using System;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;
using System.Globalization;
using HtmlRenderer;
namespace Svg.Transforms
{
    public sealed class SvgScale : SvgTransform
    {
        private float scaleFactorX;
        private float scaleFactorY;

        public float X
        {
            get { return this.scaleFactorX; }
            set { this.scaleFactorX = value; }
        }

        public float Y
        {
            get { return this.scaleFactorY; }
            set { this.scaleFactorY = value; }
        }

        public override  Matrix Matrix
        {
            get
            {
                Matrix matrix = CurrentGraphicsPlatform.CreateMatrix();
                matrix.Scale(this.X, this.Y);
                return matrix;
            }
        }

        public override string WriteToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "scale({0}, {1})", this.X, this.Y);
        }

        public SvgScale(float x) : this(x, x) { }

        public SvgScale(float x, float y)
        {
            this.scaleFactorX = x;
            this.scaleFactorY = y;
        }

		public override object Clone()
		{
			return new SvgScale(this.X, this.Y);
		}
    }
}
