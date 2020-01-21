using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;
using System.Net;

namespace WFA_MangaTestFireBest
{
    public partial class Form1 : Form
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "Ac86af7rWgd5brNJsO1Qh6ET4FrgHYIjMZl2hREn",
            BasePath = "https://testnamga.firebaseio.com"
        };
        public Form1()
        {
            InitializeComponent();
            client = new FirebaseClient(config);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var todo = new Todo
            {
                name = "Execute 1!",
                priority = 1
            };

            FirebaseResponse response = await   client.SetAsync("Todo/id"+1, todo);
            Todo todoa = response.ResultAs<Todo>(); //The response will contain the data written
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var todo = new Todo
            {
                name = "2!",
                priority = 2
            };

            FirebaseResponse response = await client.SetAsync("Todo/id/idd"+2, todo);
            Todo todoa = response.ResultAs<Todo>(); //The response will contain the data written
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var todo = new Todo
            {
                name = "Execute PUSH",
                priority = 2
            };
            PushResponse response = await client.PushAsync("todos/push", todo);
            var a=response.Result.name; //The result will contain the child name of the new data that was added
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("Todo/1");
            Todo todo = response.ResultAs<Todo>(); //The response will contain the data being retreived
            label1.Text = todo.name;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            var todo = new Todo
            {
                name = "Execute UPDATE!",
                priority = 1
            };

            FirebaseResponse response = await client.UpdateAsync("Todo/1", todo);
            Todo todo1 = response.ResultAs<Todo>(); //The response will contain the data written
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("todos"); //Deletes todos collection
            Console.WriteLine(response.StatusCode);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            //image
            FirebaseResponse response = await client.GetAsync("image/C001");
            ImageModel img = response.ResultAs<ImageModel>(); //The response will contain the data being retreived
            var request = WebRequest.Create(img.url);

            using (var responseA = request.GetResponse())
            using (var stream = responseA.GetResponseStream())
            {
                //pictureBox1.Image = Bitmap.FromStream(stream);
                //pictureBox1.LoadAsync(img.url);
                pictureBox1.Load(img.url);
            }
        }
    }
}
