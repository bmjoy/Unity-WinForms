﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    [Serializable]
    public class Label : Control
    {
#if UNITY_EDITOR
        private bool _toggleEditor = true;
#endif
        public ContentAlignment TextAlign { get; set; }

        public Label()
        {
            BackColor = Color.Transparent;
            Padding = new Forms.Padding(4, 0, 8, 0);
            Size = new Drawing.Size(128, 20);
            TabStop = false;
            TextAlign = ContentAlignment.TopLeft;

            this.SetStyle(ControlStyles.FixedHeight | ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.uwfFillRectangle(BackColor, 0, 0, Width, Height);
            g.uwfDrawString(Text, Font, ForeColor, Padding.Left, Padding.Top, Width - Padding.Right - Padding.Left, Height - Padding.Bottom - Padding.Top, TextAlign);
        }
        protected override object uwfOnPaintEditor(float width)
        {
            var control = base.uwfOnPaintEditor(width);

#if UNITY_EDITOR
            Editor.BeginVertical();
            Editor.NewLine(1);

            _toggleEditor = Editor.Foldout("Label", _toggleEditor);
            if (_toggleEditor)
            {
                Editor.BeginGroup(width - 24);

                var editorTextAlign = Editor.EnumField("      TextAlign", TextAlign);
                if (editorTextAlign.Changed) TextAlign = (ContentAlignment)editorTextAlign.Value;

                Editor.EndGroup();
            }
            Editor.EndVertical();
#endif

            return control;
        }
    }
}
