﻿//2014 Apache2, WinterDev
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LayoutFarm;
using LayoutFarm.Drawing;
using LayoutFarm.UI;


namespace TestGraphicPackage
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        static void ShowFormLayoutInspector(UISurfaceViewportControl viewport)
        {

            var formLayoutInspector = new LayoutFarm.Dev.FormLayoutInspector();
            formLayoutInspector.Show();

            formLayoutInspector.FormClosed += (s, e2) =>
            {
                formLayoutInspector = null;
            };
            formLayoutInspector.Connect(viewport);
            formLayoutInspector.Show();

        }
        private void cmdShowBasicFormCanvas_Click(object sender, EventArgs e)
        {

            UISurfaceViewportControl viewport;
            WinTimer wintimer = new MyWinTimer();
            MyRootGraphic rootgfx = new MyRootGraphic(wintimer, 800, 600);

            Form formCanvas = FormCanvasHelper.CreateNewFormCanvas(rootgfx, new MyUserInputEventBridge(), out viewport);
            viewport.PaintMe();
            formCanvas.Show();
            ShowFormLayoutInspector(viewport);
        }

        private void cmdShowEmbededViewport_Click(object sender, EventArgs e)
        {
            Form simpleForm = new Form();
            simpleForm.Text = "SimpleForm2";
            simpleForm.WindowState = FormWindowState.Maximized;
            Rectangle screenClientAreaRect = Screen.PrimaryScreen.WorkingArea;
            UISurfaceViewportControl viewport = new UISurfaceViewportControl();
            viewport.Bounds = new Rectangle(0, 0, screenClientAreaRect.Width, screenClientAreaRect.Height);
            simpleForm.Controls.Add(viewport);

            WinTimer wintimer = new MyWinTimer();
            MyRootGraphic rootgfx = new MyRootGraphic(wintimer, 800, 600);
            viewport.InitRootGraphics(800, 600, new MyUserInputEventBridge(), rootgfx);
            viewport.PaintMe();

            simpleForm.Show();

            ShowFormLayoutInspector(viewport);
        }
    }
}