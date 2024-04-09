using ISSLab.View;
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


namespace ISSLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPosts();
        }

        private void LoadPosts()
        {
            int numberOfPostsToAdd = 5;

            for (int i = 0; i < numberOfPostsToAdd; i++)
            {
                PostContent postContent = new PostContent();
                PostsGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid.SetRow(postContent, i);
                PostsGrid.Children.Add(postContent);
            }
        }
    }
}