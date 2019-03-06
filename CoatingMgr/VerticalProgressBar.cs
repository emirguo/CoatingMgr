using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    class VerticalProgressBar : ProgressBar
    {
        public VerticalProgressBar()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }

        //重写OnPaint方法
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush brush = null;
            Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
            bounds.Width -= 4;
            bounds.Height = ((int)(bounds.Height * (((double)base.Value) / ((double)base.Maximum)))) - 4;
            brush = new SolidBrush(Color.Gray);
            e.Graphics.FillRectangle(brush, 2, base.Height - bounds.Height - 2, bounds.Width, bounds.Height);

            SolidBrush brushLine = new SolidBrush(base.BackColor);
            e.Graphics.FillRectangle(brushLine, 2, (int)(base.Height/3), base.Width - 4, 2);
            e.Graphics.FillRectangle(brushLine, 2, (int)(base.Height * 2/ 3), base.Width - 4, 2);
        }
    }
}
