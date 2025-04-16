using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Courses.DAL.Data;
using Courses.DAL.Models;
using Courses.WPF.Command;

namespace Courses.WPF.ViewModel
{
    public class GroupsViewModel : ViewModelBase
    {
        private readonly IGroupDataProvider _groupDataProvider;
        private readonly ITeacherDataProvider _teacherDataProvider;
        private readonly IStudentDataProvider _studentDataProvider;
        private GroupItemViewModel? _selectedGroup;
        private TeacherItemViewModel? _selectedTeacher;
        private GroupItemViewModel _groupStudents;

        public GroupsViewModel(IGroupDataProvider groupDataProvider, ITeacherDataProvider teacherDataProvider, IStudentDataProvider studentDataProvider)
        {
            _groupDataProvider = groupDataProvider;
            _teacherDataProvider = teacherDataProvider;
            _studentDataProvider = studentDataProvider;
            CreateCommand = new DelegateCommand(Create);
            SaveCommand = new DelegateCommand(async (param) => await SaveAsync(param));
            ClearGroupCommand = new DelegateCommand(async (param) => await ClearGroupAsync(param), CanClearGroup);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            ImportStudentsCommand = new DelegateCommand(ImportStudents);
            ExportStudentsCommand = new DelegateCommand(async (parameter) => await ExportStudentsAsync(parameter));
        }

        public ObservableCollection<GroupItemViewModel> Groups { get; } = new();
        public ObservableCollection<TeacherItemViewModel> Teachers { get; } = new();
        public ObservableCollection<StudentItemViewModel>? Students { get; } = new();
        public GroupItemViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
                RaisePropertyChanged(nameof(SelectedGroup.StudentCount));
                RaisePropertyChanged(nameof(SelectedGroup.Students));
                DeleteCommand.RaiseCanExecuteChanged();
                ClearGroupCommand.RaiseCanExecuteChanged();

                if (_selectedGroup?.Teacher != null)
                {
                    SelectedTeacher = Teachers.FirstOrDefault(t => t.TeacherId.Equals(_selectedGroup.Teacher.TeacherId));
                }
                else
                {
                    SelectedTeacher = null;
                }
            }
        }

        public TeacherItemViewModel? SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
            }
        }

        public bool IsGroupSelected => SelectedGroup is not null;

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearGroupCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand ExportStudentsCommand { get; }
        public DelegateCommand ImportStudentsCommand { get; }

        public async override Task LoadAsync()
        {
            if (Groups.Any() && Teachers.Any())
            {
                return;
            }

            var groups = await _groupDataProvider.GetAllAsync();

            if (groups is not null)
            {
                Groups.Clear();
                foreach (var group in groups)
                {
                    Groups.Add(new GroupItemViewModel(group));
                }
            }
            await LoadTeachersAsync();
            await LoadGroupStudentsAsync();
        }

        private async Task LoadTeachersAsync()
        {
            Teachers.Clear();

            var teachers = await _teacherDataProvider.GetAllAsync();
            if (teachers is not null)
            {
                foreach (var teacher in teachers)
                {
                    Teachers.Add(new TeacherItemViewModel(teacher));
                }
            }
        }

        private async Task LoadGroupStudentsAsync()
        {
            if (Students != null)
            {
                Students.Clear();

                var students = await _studentDataProvider.GetAllAsync();
                Debug.WriteLine($"students in LoadGroupStudentsAsync: {students}");

                if (students is not null)
                {
                    foreach (var student in students)
                    {
                        Students.Add(new StudentItemViewModel(student));
                    }
                }
            }
        }
        
        private async Task ExportStudentsAsync(object? parameter)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Please select a group first.", "Export", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
                StudentsGroup studentsGroup = await _groupDataProvider.GetGroupWithStudentsAsync(SelectedGroup.Id);
                SelectedGroup = MapStudentsGroupToGroupItemViewModel(studentsGroup);
            
            if (SelectedGroup == null)
            {
                MessageBox.Show("Group not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedGroup.Students == null || !SelectedGroup.Students.Any())
            {
                Debug.WriteLine($"SelectedGroup.Students: {SelectedGroup.Students}");
                MessageBox.Show("No students to export.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"Students_{SelectedGroup.Name}.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("StudentId,FirstName,LastName");
                        foreach (var student in SelectedGroup.Students)
                        {
                            writer.WriteLine($"{student.StudentId},{student.FirstName},{student.LastName}");
                        }
                    }
                    MessageBox.Show("Export successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting students: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void ImportStudents(object? parameter)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Please select a group first.", "Import", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var studentsToAdd = new List<Student>();
                    var processedStudentIds = new HashSet<int>();

                    using (var reader = new StreamReader(openFileDialog.FileName))
                    {
                        string? line;
                        bool isFirstLine = true;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (isFirstLine)
                            {
                                isFirstLine = false;
                                continue;
                            }

                            var values = line.Split(',');
                            if (values.Length == 3)
                            {
                                if (int.TryParse(values[0], out int studentId))
                                {
                                    if (processedStudentIds.Contains(studentId))
                                    {
                                        MessageBox.Show($"Duplicate StudentId: {studentId} in CSV. Skipping.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        continue;
                                    }
                                    processedStudentIds.Add(studentId);

                                    var student = new Student
                                    {
                                        StudentId = studentId,
                                        FirstName = values[1],
                                        LastName = values[2],
                                        GroupId = SelectedGroup.Model.Id
                                    };

                                    studentsToAdd.Add(student);
                                }
                                else
                                {
                                    MessageBox.Show($"Invalid StudentId: {values[0]} in CSV. Skipping.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Invalid CSV line: {line}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    
                    SelectedGroup.Model.Students.Clear();
                    foreach (var student in studentsToAdd)
                    {
                        SelectedGroup.Model.Students.Add(student);
                    }

                    await _groupDataProvider.UpdateAsync(SelectedGroup.Model);
                    SelectedGroup.Students.Clear();
                    foreach (var student in SelectedGroup.Model.Students)
                    {
                        SelectedGroup.Students.Add(new StudentItemViewModel(student));
                    }

                    RaisePropertyChanged(nameof(SelectedGroup.Students));

                    MessageBox.Show("Import successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error importing students: {ex.Message}");
                    Debug.WriteLine(ex.StackTrace);
                    MessageBox.Show($"Error importing students: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Create(object? parameter)
        {
            var group = new StudentsGroup { Name = "New group name" };
            var viewModel = new GroupItemViewModel(group);
            Groups.Add(viewModel);
            SelectedGroup = viewModel;
        }

        private async Task SaveAsync(object? parameter)
        {
            if (SelectedGroup == null)
                return;

            try
            {
                if (SelectedTeacher != null)
                {
                    Debug.WriteLine($"Assigning TeacherId: {SelectedTeacher.TeacherId}");
                    SelectedGroup.TeacherId = SelectedTeacher.TeacherId;
                    SelectedGroup.Teacher = SelectedTeacher.Model;
                }
                else
                {
                    Debug.WriteLine("No teacher selected, setting TeacherId to null.");
                    SelectedGroup.TeacherId = null;
                }

                if (SelectedGroup.Model.Id == 0)
                {
                    Debug.WriteLine("Adding a new group to the database...");
                    Debug.WriteLine($"Selected Teacher: {SelectedTeacher}");
                    Debug.WriteLine($"Selected TeacherId: {SelectedTeacher?.TeacherId}");
                    Debug.WriteLine($"Group TeacherId before save: {SelectedGroup.TeacherId}");
                    await _groupDataProvider.AddAsync(SelectedGroup.Model);
                    await _groupDataProvider.ReloadAsync(SelectedGroup.Model);
                    Debug.WriteLine($"New group saved! Assigned ID: {SelectedGroup.Model.Id}");
                    RaisePropertyChanged(nameof(SelectedGroup));
                }
                else
                {
                    Debug.WriteLine($"Updating existing group with ID: {SelectedGroup.Model.Id}");
                    await _groupDataProvider.UpdateAsync(SelectedGroup.Model);
                }

                await _groupDataProvider.UpdateAsync(SelectedGroup.Model);
                RaisePropertyChanged(nameof(Groups));
                RaisePropertyChanged(nameof(SelectedGroup));
                ClearGroupCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving group: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private void Delete(object? parameter)
        {
            if (SelectedGroup is not null)
            {
                Groups.Remove(SelectedGroup);
                SelectedGroup = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedGroup is not null;

        private GroupItemViewModel? MapStudentsGroupToGroupItemViewModel(StudentsGroup? studentsGroup)
        {
            if (studentsGroup == null)
            {
                return null;
            }

            return new GroupItemViewModel(studentsGroup);
        }

        public async Task ClearGroupAsync(object? parameter = null)
        {
            if (SelectedGroup == null) return;

            try
            {
                Debug.WriteLine($"Clearing students for group: {SelectedGroup.Model.Id}");

                SelectedGroup.Model.Students.Clear();

                await _groupDataProvider.UpdateAsync(SelectedGroup.Model);

                Debug.WriteLine("Group cleared successfully!");
                RaisePropertyChanged(nameof(SelectedGroup));
                RaisePropertyChanged(nameof(Groups));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error clearing group: {ex.Message}");
            }
        }

        private bool CanClearGroup(object? parameter) => SelectedGroup?.Model.Students?.Any() == true;

        private async Task LoadSelectedGroupAsync(int groupId)
        {
            var group = await _groupDataProvider.GetGroupWithStudentsAsync(groupId);

            if (group != null)
            {
                SelectedGroup = new GroupItemViewModel(group);
                RaisePropertyChanged(nameof(SelectedGroup));
            }
        }
    }
}
