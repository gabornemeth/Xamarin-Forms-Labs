using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XLabs.Samples.Model
{
    public class TestJob : ObservableCollection<TestPerson>
    {
        public string Name { get; set; }
        private Random _random = new Random();
        internal TestOrganization Organization { get; set; }

        public TestJob()
        {
            AddPersonCommand = new Command(AddPerson);
            RemoveCommand = new Command(() =>
            {
                if (Organization != null)
                {
                    Organization.Jobs.Remove(this);
                }
            });
        }

        public void AddPerson()
        {
            // add new person
            Add(new TestPerson
            {
                Age = _random.Next(20, 80),
                FirstName = $"Joe #{Count + 1}",
                LastName = "Average",
                Job = this
            });
        }

        /// <summary>
        /// Removes this job from the organization, if there is one assigned.
        /// </summary>
        public ICommand RemoveCommand
        {
            get; private set;
        }

        public ICommand AddPersonCommand { get; private set; }
    }
}
