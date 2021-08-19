using System;
using System.Threading;
using System.Windows.Forms;

namespace Share.Widget.DotNetBar.ViewUI
{
    public partial class FrmLoading : DevComponents.DotNetBar.Office2007Form
    {
        //**------------------------------------------------------------------

        #region Member
        delegate void updateUI(string mess);
        delegate void CloseForm();
        private updateUI _updateUI;
        private CloseForm _closeForm;
        #endregion

        //**------------------------------------------------------------------

        #region Constructor
        public FrmLoading()
        {
            InitializeComponent();
            Initialized();
        }
        public FrmLoading(SynchronizationContext SyncContext, Action T)
        {
            InitializeComponent();
            Initialized();
            new Thread(new ThreadStart(delegate
            {
                SyncContext.Send(state =>
                {
                    T();
                }, null);
                Thread.Sleep(50);
                _closeForm();
            })).Start();
        }
        public FrmLoading(Action T)
        {
            InitializeComponent();
            Initialized();
            new Thread(new ThreadStart(delegate
            {
                T();
                Thread.Sleep(50);
                _closeForm();
            })).Start();
        }
        //public FrmLoading(Action<object,object> T)
        //{
        //    InitializeComponent();
        //    Initialized();
        //    new Thread(new ThreadStart(delegate
        //    {
        //        T();
        //        _closeForm();
        //    })).Start();
        //}
        #endregion

        //**------------------------------------------------------------------

        #region Private Method
        public void Initialized()
        {
            this.ShowInTaskbar = false;
            _updateUI = new updateUI(UpdateMessage);
            _closeForm = new CloseForm(CloseFormAction);
        }
        public void CloseFormAction()
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = () => CloseFormAction();
                Invoke(invoker);
                return;
            }
            this.Close();
        }
        public void UpdateMessage(string text)
        {

            if (InvokeRequired)
            {
                MethodInvoker invoker = () => UpdateMessage(text);
                Invoke(invoker);
                return;
            }

            lbltitle.Text = text;
        }
        public void SetText(string text)
        {
            _updateUI(text);
        }
        #endregion

        //**------------------------------------------------------------------

        #region Event Control
        private void FrmLoading_Load(object sender, EventArgs e)
        {

        }

        public void ShowLoading(string message)
        {
            _updateUI(message);
            new Thread(new ThreadStart(delegate {  this.ShowDialog(); })).Start();
        }
        #endregion

        //**------------------------------------------------------------------
    }
}
