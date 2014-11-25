using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Collections.ObjectModel;
using System.IO;




namespace SurfaceApplication1
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 

        private ObservableCollection<PhotoData> libraryItems = new ObservableCollection<PhotoData>();
        private ObservableCollection<PhotoData> scatterItems = new ObservableCollection<PhotoData>();
        private string myPath; //used for video
        private Stack<Stroke> erasedStrokes = new Stack<Stroke>(); //for undo-ing ink canvas strokes

        public ObservableCollection<PhotoData> LibraryItems
        {
            get { return libraryItems; }
        }

        public ObservableCollection<PhotoData> ScatterItems
        {
            get { return scatterItems; }
        }

        public SurfaceWindow1()
        {
            //sets current directory for video
            myPath = System.IO.Directory.GetCurrentDirectory();
            myPath = myPath.Remove(myPath.IndexOf("bin\\Debug"));

            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            //handler for fiducial tag recognition
            TouchDown += new EventHandler<TouchEventArgs>(SurfaceWindow1_TouchDown);
        }

        //PhotoData Class holds information about initializing images
        public class PhotoData
        {

            public string Source { get; private set; }
            public string Caption { get; private set; }

            public PhotoData(string source, string caption)
            {
                this.Source = source;
                this.Caption = caption;
            }
        }

        //class takes a screenshot of the user's collage in scatterview
        class ScreenshotTaker
        {
            private static int count = 0;

            public static void TakeScreenshot(Visual target)
            {
                if (count == 16) //Save at most 16 images. Override previous images if more than 16 screenshots are taken
                    count = 0;

           
                string fileName = "screenshot" + count + ".tiff";
                Console.WriteLine("Try to take screenshot: " + fileName);
                FileStream stream = new FileStream(fileName, FileMode.Create);
                TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(GetScreenShot(target)));
                encoder.Save(stream);
                stream.Flush();
                stream.Close();
                Console.WriteLine("Screenshot taken");
                count++;
            }

            private static BitmapSource GetScreenShot(Visual target)
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
                RenderTargetBitmap bitmap = new RenderTargetBitmap(1024, 768, 96, 96, PixelFormats.Pbgra32);

                DrawingVisual drawingvisual = new DrawingVisual();

                using (DrawingContext context = drawingvisual.RenderOpen())
                {
                    context.DrawRectangle(new VisualBrush(target), null, new Rect(new Point(), bounds.Size));
                    context.Close();
                }

                bitmap.Render(drawingvisual);
                return bitmap;
            }

        }

        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = this;

            //adding images to library of items

            LibraryItems.Add(new PhotoData("Images/statue.jpg", "Panther Statue"));
            LibraryItems.Add(new PhotoData("Images/boston.jpg", "Boston"));
         
            LibraryItems.Add(new PhotoData("Images/utah2.jpg", "Rock Forms"));
            LibraryItems.Add(new PhotoData("Images/kayak.JPG", "Lake Tahoe"));
            LibraryItems.Add(new PhotoData("Images/utah.jpg", "Utah"));

            LibraryItems.Add(new PhotoData("Images/thailand.JPG", "Thailand"));
            LibraryItems.Add(new PhotoData("Images/jump.jpg", "Tahoe"));
            LibraryItems.Add(new PhotoData("Images/taj.jpg", "Taj Mahal"));
            LibraryItems.Add(new PhotoData("Images/greexe.jpg", "Greece"));
            LibraryItems.Add(new PhotoData("Images/giza.jpg", "Egypt"));
            LibraryItems.Add(new PhotoData("Images/nevada.jpg", "Nevada"));
            LibraryItems.Add(new PhotoData("Images/hollywood.jpg", "Hollywood"));
            LibraryItems.Add(new PhotoData("Images/sydney.jpg", "Sydney"));
            viewList.ItemsSource = LibraryItems;
        }
    
        //responds to the fiducial tags by changing the background color, according to each tag
        void SurfaceWindow1_TouchDown(object sender, TouchEventArgs e)
        {
            TouchDevice c = e.TouchDevice;
      
            if (c.GetIsTagRecognized() == true)
            { 
                if(c.GetTagData().Value == 1) //for tag with value 1, change background color to light green
                    scatter.Background = Brushes.LightGreen;
                else if (c.GetTagData().Value == 2) //for tag with value 2, change background color to light green
                    scatter.Background = Brushes.Pink;
                else if (c.GetTagData().Value == 3) //for tag with value 3, change background color to light green
                    scatter.Background = Brushes.Navy;
                else //for another other tag, change background color to light green
                    scatter.Background = Brushes.BlueViolet;
            }
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }


        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
            ClearButton.Click += new RoutedEventHandler(ClearButton_Click);
            SaveButton.Click += new RoutedEventHandler(SaveButton_Click);
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void InkCanvas_StrokeErasing(object sender, SurfaceInkCanvasStrokeErasingEventArgs e)
        {
            if (!e.Cancel)
            {
                erasedStrokes.Push(e.Stroke);
            }
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
        // Handle the action for the click here.
            scatter.Items.Clear();
            myInkCanvas.Strokes.Clear();
        }

        private void PaintButtonOn_Click(object sender, RoutedEventArgs e)
        {
            //turns on paint canvas
            myInkCanvas.Visibility = Visibility.Visible; 
            myInkCanvas.EditingMode = SurfaceInkEditingMode.Ink; //activate ink
        }

        private void PaintButtonOff_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = SurfaceInkEditingMode.None; //don't allow ink to show
        }

        //deletes previous strokes
        private void SurfaceButtonEraseStroke_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            myInkCanvas.Strokes.Clear(); 
        }

        //takes a screenshot of collage
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the action for the click here.
 
            ScreenshotTaker.TakeScreenshot((Visual) scatter);  //original
        }

        //puts video into scatterview
        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {
           // string targetVideo = myPath + "Images/Wildlife.wmv";
            string targetVideo = myPath + "Images/Demo3.wmv";

            // Create a ScatterViewItem control and add it to the Items collection.
            ScatterViewItem item = new ScatterViewItem();
            scatter.Items.Add(item);

            // Create a MediaElement object.
            MediaElement video = new MediaElement();
            video.LoadedBehavior = MediaState.Manual;

            // The media dimensions are not available until the MediaOpened event.
            video.MediaOpened += delegate
            {
                // Sets the position of the video to be in the center.   
                item.Height = 450; 
                item.Width = 750;
                item.Orientation = 0;
                item.Center = new Point(675, 300);
            };

            // Set the Content to the video.
            item.Content = video;

            // Get the video if it exists.
            if (System.IO.File.Exists(targetVideo))
            {
                video.Source = new Uri(targetVideo, UriKind.Relative);
                video.Play();
            }
            else
            {
                item.Content = "Video not found";
            }   
        }
      

        private void StackPanel_TouchDown(object sender, TouchEventArgs e)
        {
            //for image - adds image from the panel above to the collage area below
            Image image = new Image();
            sender.GetType();
            Image photo = (Image)sender;

            



            String uri = photo.Source.ToString();
            String junk = "pack://application:,,,/TangibleStories2;component/"; 
            int index = junk.Length;
            String actualUri = uri.Substring(index);
            image.Source = new BitmapImage(new Uri(actualUri, UriKind.Relative));
            scatter.Items.Add(image);
        }
    }
}