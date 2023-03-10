using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using Template2.Domain.Entities;
using Template2.Domain.Repositories;
using Template2.Infrastructure;

namespace Template2.WPF.ViewModels
{
    public class Sample005ViewModel : ViewModelBase
    {
        //// 外部接触Repository
        private IWorkerGroupMstRepository _workerGroupMstRepository;
        private IWorkerMstRepository _workerMstRepository;

        public Sample005ViewModel()
            : this(Factories.CreateWorkerGroupMst(), Factories.CreateWorkerMst())
        {

        }

        public Sample005ViewModel(IWorkerGroupMstRepository workerGroupMstRepository, 
                                  IWorkerMstRepository workerMstRepository)
        {
            _workerGroupMstRepository = workerGroupMstRepository;
            _workerMstRepository = workerMstRepository;

            //// DelegateCommandメソッドを登録
            WorkerGroupTreeViewSelectedItemChanged = new DelegateCommand(WorkerGroupTreeViewSelectedItemChangedExecute);

            WorkerGroupTreeViewData.CreateTreeView(ref _workerGroupTreeView,
                                                   _workerGroupMstRepository.GetData(), 
                                                   _workerMstRepository.GetData());
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        private ObservableCollection<WorkerGroupTreeViewData> _workerGroupTreeView
            = new ObservableCollection<WorkerGroupTreeViewData>();
        public ObservableCollection<WorkerGroupTreeViewData> WorkerGroupTreeView
        {
            get { return _workerGroupTreeView; }
            set { SetProperty(ref _workerGroupTreeView, value); }
        }

        private string _workerGroupCodeText;
        public string WorkerGroupCodeText
        {
            get { return _workerGroupCodeText; }
            set { SetProperty(ref _workerGroupCodeText, value); }
        }

        private string _workerCodeText;
        public string WorkerCodeText
        {
            get { return _workerCodeText; }
            set { SetProperty(ref _workerCodeText, value); }
        }

        private WorkerGroupTreeViewData _workerGroupTreeViewSelectedItem;
        public WorkerGroupTreeViewData WorkerGroupTreeViewSelectedItem
        {
            get { return _workerGroupTreeViewSelectedItem; }
            set { SetProperty(ref _workerGroupTreeViewSelectedItem, value); }
        }

        #endregion

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        public DelegateCommand WorkerGroupTreeViewSelectedItemChanged { get; }

        private void WorkerGroupTreeViewSelectedItemChangedExecute()
        {
            if (WorkerGroupTreeViewSelectedItem == null)
            {
                return;
            }

            if (WorkerGroupTreeViewSelectedItem.UpperTreeViewData == null)
            {
                //// 親TreeViewDataが無い = 親データを選択している場合
                WorkerGroupCodeText = WorkerGroupTreeViewSelectedItem.Id;
                WorkerCodeText = String.Empty;
            }
            else
            {
                //// 親TreeViewDataが有る = 子データを選択している場合
                WorkerGroupCodeText = WorkerGroupTreeViewSelectedItem.UpperTreeViewData.Id;
                WorkerCodeText = WorkerGroupTreeViewSelectedItem.Id;
            }
        }

        #endregion

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        #endregion

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// Screen transition
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _mainWindowViewModel.ViewOutline = "> サンプル005（TreeViewと選択アイテムのバインド）";
        }

        #endregion
    }
}
