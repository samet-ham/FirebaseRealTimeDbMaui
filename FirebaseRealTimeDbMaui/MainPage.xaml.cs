using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace FirebaseRealTimeDbMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public ObservableCollection<TodoItem> TodoItems { get; set; } = new ObservableCollection<TodoItem>();
        FirebaseClient firebaseClient = new FirebaseClient("https://minimal-social-app-d11a1-default-rtdb.europe-west1.firebasedatabase.app/");

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            var collection = firebaseClient
                            .Child("Todo")
                            .AsObservable<TodoItem>()
                            .Subscribe((item) =>
                            {
                                if (item.Object != null)
                                {
                                    TodoItems.Add(item.Object);
                                }
                            });
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            firebaseClient.Child("Todo").PostAsync(new TodoItem
            {
                Title = TitleEntry.Text,
            });
        }
    }
}