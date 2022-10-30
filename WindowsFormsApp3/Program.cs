using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp3
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }
    /* public enum ProgressBarDisplayText
     {
         Percentage,
         CustomText
     }
     public class ProgressBarEx : ProgressBar
     { 
         public ProgressBarDisplayText DisplayStyle { get; set; }


         public String CustomText { get; set; }
         public ProgressBarEx()
         {

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
         }

         protected override void OnPaint(PaintEventArgs e)
         {
             Rectangle rect = ClientRectangle;
             Graphics g = e.Graphics;

             ProgressBarRenderer.DrawHorizontalBar(g, rect);
             rect.Inflate(-3, -3);
             if (Value > 0)
             {
                 // As we doing this ourselves we need to draw the chunks on the progress bar
                 Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                 ProgressBarRenderer.DrawHorizontalChunks(g, clip);
             }

             // Set the Display text (Either a % amount or our custom text
             int percent = (int)(((double)this.Value / (double)this.Maximum) * 100);
             string text = DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' : CustomText;

             using (Font f = new Font(FontFamily.GenericSerif, 10))
             {

                 SizeF len = g.MeasureString(text, f);
                 // Calculate the location of the text (the middle of progress bar)
                 // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
                 Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                 // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
                 // Draw the custom text
                 g.DrawString(text, f, Brushes.Red, location);
             }

             LinearGradientBrush brush = null;
             Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
             double scaleFactor = (((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum));

             if (ProgressBarRenderer.IsSupported)
                 ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);

             rec.Width = (int)((rec.Width * scaleFactor) - 4);
             rec.Height -= 4;
             brush = new LinearGradientBrush(rec, this.ForeColor, this.BackColor, LinearGradientMode.Vertical);
             e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
         }
     }*/
    //Property to set to decide whether to print a % or Text123123123123
    public class ProgressBarEx : ProgressBar
    {
        public ProgressBarDisplayText DisplayStyle { get; set; }

        //Property to hold the custom text
        public String CustomText { get; set; }

        public ProgressBarEx()
        {
            // Modify the ControlStyles flags
            //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-3, -3);
            if (Value > 0)
            {
                // As we doing this ourselves we need to draw the chunks on the progress bar
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // Set the Display text (Either a % amount or our custom text
            int percent = (int)(((double)this.Value / (double)this.Maximum) * 100);
            string text =/*DisplayStyle == ProgressBarDisplayText.Percentage ? percent.ToString() + '%' :*/ CustomText;

            using (Font f = new Font(FontFamily.GenericSerif, 10))
            {

                SizeF len = g.MeasureString(text, f);
                // Calculate the location of the text (the middle of progress bar)
                // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
                Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
                // Draw the custom text
                g.DrawString(text, f, Brushes.Black, location);
            }
        }
        internal static class Program
        {
            /// <summary>
            /// Главная точка входа для приложения.
            /// </summary>
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
