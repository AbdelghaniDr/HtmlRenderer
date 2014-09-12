﻿//2014 Apache2, WinterDev
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LayoutFarm.Presentation
{
    public class UIRootGraphic : RootGraphic
    {
        List<RenderElementRequest> veReqList = new List<RenderElementRequest>();
        static Stack<LayoutPhaseVisitor> visualArgStack = new Stack<LayoutPhaseVisitor>();

        public UIRootGraphic()
        {
#if DEBUG
            dbugCurrentGlobalVRoot = this;
            dbug_Init();
#endif
        }
#if DEBUG
        ~UIRootGraphic()
        {
            dbugHitTracker.Close();
        }
#endif
        static Point localCaretPos;
        static RenderElement caretOwner;
        public static Point GetGlobalCaretPosition()
        {
            if (caretOwner == null)
            {
                return Point.Empty;
            }
            Point caretPos = localCaretPos;
            Point globalCaret = caretOwner.GetGlobalLocation();
            caretPos.Offset(globalCaret.X, globalCaret.Y);
            return caretPos;
        }
        public static void SetCarentPosition(Point p, RenderElement owner)
        {
            caretOwner = owner;
            localCaretPos = p;
        
        }
        static void vinv_SetWinRoot(TopWindowRenderBox winroot)
        {

        }
        public static LayoutPhaseVisitor GetVisualInvalidateArgs(TopWindowRenderBox winroot)
        {
            if (visualArgStack.Count > 0)
            {
                LayoutPhaseVisitor vinv = visualArgStack.Pop();
                vinv_SetWinRoot(winroot);
                return vinv;
            }
            else
            {
                return new LayoutPhaseVisitor(winroot);
            }
        }

         

        public const int IS_SHIFT_KEYDOWN = 1 << (1 - 1);
        public const int IS_ALT_KEYDOWN = 1 << (2 - 1);
        public const int IS_CTRL_KEYDOWN = 1 << (3 - 1);


        public int VisualRequestCount
        {
            get
            {
                return veReqList.Count;
            }
        }

        public void ClearVisualRequests(TopWindowRenderBox winroot)
        {
            int j = veReqList.Count;
            for (int i = 0; i < j; ++i)
            {
                RenderElementRequest req = veReqList[i];
                switch (req.req)
                {

                    case RequestCommand.AddToWindowRoot:
                        {
                            winroot.AddChild(req.ve);

                        } break;
                    case RequestCommand.DoFocus:
                        {
                            RenderElement ve = req.ve;
                            if (ve.WinRoot != null)
                            {
                                ve.WinRoot.CurrentKeyboardFocusedElement = ve;
                                LayoutPhaseVisitor vinv = ve.GetVInv();
                                ve.InvalidateGraphic();
                                ve.FreeVInv();
                            }
                        } break;
                    case RequestCommand.InvalidateArea:
                        {
                            Rectangle r = (Rectangle)req.parameters;

                            InternalRect internalRect = InternalRect.CreateFromRect(r);
                            winroot.InvalidateGraphicArea(req.ve, internalRect);
                            InternalRect.FreeInternalRect(internalRect);

                        } break;

                }
            }
            veReqList.Clear();
        }
    }
}