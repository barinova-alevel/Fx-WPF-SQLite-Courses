using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Courses.DAL.Data;
using Courses.DAL.Models;
using Courses.WPF.Command;
using Microsoft.EntityFrameworkCore;

namespace Courses.WPF.ViewModel
{
    public class GroupsViewModel : ViewModelBase
    {
        private readonly IGroupDataProvider _groupDataProvider;
        private readonly ITeacherDataProvider _teacherDataProvider;
        private GroupItemViewModel? _selectedGroup;
        private TeacherItemViewModel? _selectedTeacher;

        public GroupsViewModel(IGroupDataProvider groupDataProvider, ITeacherDataProvider teacherDataProvider)
        {
            _groupDataProvider = groupDataProvider;
            _teacherDataProvider = teacherDataProvider;
            ImportCommand = new DelegateCommand(Import);
            CreateCommand = new DelegateCommand(Create);
            //SaveCommand = new DelegateCommand(Save);
            SaveCommand = new DelegateCommand(async(param) => await SaveAsync(param));
            //ClearGroupCommand = new DelegateCommand(ClearGroup, CanClearGroup);
            ClearGroupCommand = new DelegateCommand(async (param) => await ClearGroupAsync(param), CanClearGroup);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        public ObservableCollection<GroupItemViewModel> Groups { get; } = new();
        public ObservableCollection<TeacherItemViewModel> Teachers { get; } = new();
        public ObservableCollection<StudentItemViewModel>? Students { get; }
        public GroupItemViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
                RaisePropertyChanged(nameof(SelectedGroup.StudentCount));
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

        public DelegateCommand ImportCommand { get; }
        public DelegateCommand CreateCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearGroupCommand { get; }
        public DelegateCommand DeleteCommand { get; }

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

        public void Import(object? parameter)
        {
            throw new NotImplementedException();
        }

        private void Create(object? parameter)
        {
            var group = new StudentsGroup { Name = "New group name" };
            var viewModel = new GroupItemViewModel(group);
            Groups.Add(viewModel);
            SelectedGroup = viewModel;
        }

        //private async void Save(object? parameter)
        //{
        //    if (SelectedGroup == null)
        //        return;

        //    try
        //    {
        //        if (SelectedTeacher != null)
        //        {
        //            Debug.WriteLine($"Assigning TeacherId: {SelectedTeacher.TeacherId}");
        //            SelectedGroup.TeacherId = SelectedTeacher.TeacherId;
        //            SelectedGroup.Teacher = SelectedTeacher.Model;
        //        }
        //        else
        //        {
        //            Debug.WriteLine("No teacher selected, setting TeacherId to null.");
        //            SelectedGroup.TeacherId = null;
        //        }

        //        if (SelectedGroup.Model.Id == 0)
        //        {
        //            Debug.WriteLine("Adding a new group to the database...");
        //            Debug.WriteLine($"Selected Teacher: {SelectedTeacher}");
        //            Debug.WriteLine($"Selected TeacherId: {SelectedTeacher?.TeacherId}");
        //            Debug.WriteLine($"Group TeacherId before save: {SelectedGroup.TeacherId}");
        //            await _groupDataProvider.AddAsync(SelectedGroup.Model);
        //            await _groupDataProvider.ReloadAsync(SelectedGroup.Model);
        //            Debug.WriteLine($"New group saved! Assigned ID: {SelectedGroup.Model.Id}");
        //            RaisePropertyChanged(nameof(SelectedGroup));
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"Updating existing group with ID: {SelectedGroup.Model.Id}");
        //            await _groupDataProvider.UpdateAsync(SelectedGroup.Model);
        //        }

        //        RaisePropertyChanged(nameof(Groups));
        //        RaisePropertyChanged(nameof(SelectedGroup));
        //        ClearGroupCommand.RaiseCanExecuteChanged();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Error saving group: {ex.Message}");
        //        if (ex.InnerException != null)
        //        {
        //            Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        //        }
        //    }
        //}
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
                //ClearGroupCommand.RaiseCanExecuteChanged();
                ((DelegateCommand)ClearGroupCommand).RaiseCanExecuteChanged();
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

        //public async void ClearGroup(object? parameter)
        //{
        //    if (SelectedGroup?.Model.Students != null)
        //    {
        //        SelectedGroup.Model.Students.Clear();
        //        SelectedGroup.UpdateStudentCount();
        //        await SaveAsync(parameter);
        //        RaisePropertyChanged(nameof(SelectedGroup));
        //        ClearGroupCommand.RaiseCanExecuteChanged();
        //    }
        //}
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
    }
}
