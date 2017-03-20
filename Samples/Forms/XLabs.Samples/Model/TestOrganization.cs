using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Data;

namespace XLabs.Samples.Model
{
    public class TestOrganization : ObservableObject
    {
        private readonly string[] _availableJobs = new[] { "Developer", "Accountant", "Manager" };
        private Random _random = new Random();

        public IList<TestJob> Jobs { get; } = new ObservableCollection<TestJob>();

        public TestOrganization()
        {
            AddJobCommand = new Command(() =>
            {
                var job = new TestJob
                {
                    Name = _availableJobs[_random.Next(0, _availableJobs.Length)],
                    Organization = this
                };
                // maybe add some persons to the job
                var numberOfPersons = _random.Next(0, 4);
                for (var i = 0; i < numberOfPersons; i++)
                    job.AddPerson();
                Jobs.Add(job); // Add new job
            });
        }

        public ICommand AddJobCommand { get; private set; }
    }
}
