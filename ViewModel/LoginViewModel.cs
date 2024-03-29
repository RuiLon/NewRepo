using Demo.Common;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Demo.Model;
using Demo.DataAccess;
using MySqlConnector;
using Demo.DataAccess.Bean;


namespace Demo.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        public CommandBase CloseWindowsCommand { get; set; }
        
        public CommandBase LoginCommand { get; set; }   

        public LoginModel LoingModel { get; set; } = new LoginModel();

        public static DataBaseManager dataBase = DataBaseManager.GetInstance();


        private string _errorMessage ;
        
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; this.DoNotify(); }
        }

        public LoginViewModel()
        {   
            this.CloseWindowsCommand = new CommandBase(new Action<object>((o)=>
            {
                (o as Window).Close();
            }),new Func<object,bool>((o) =>
            {
                return true;
            }));
            this.LoginCommand = new CommandBase();
            this.LoginCommand.DoExecute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
            dataBase.close();
        }

        private void DoLogin(object o)
        {
            this.ErrorMessage = "";
            if (string.IsNullOrEmpty(LoingModel.UserName))
            {
                this.ErrorMessage = "请输入用户名！";
                return;
            }
            if (string.IsNullOrEmpty(LoingModel.Password))
            {
                this.ErrorMessage = "请输入密码！";
                return;
            }
            if (!dataBase.open())
            {
                this.ErrorMessage = "连接数据库失败";
                return;
            }
            
            string sql = $"select * from tbl_user where uname = '{LoingModel.UserName}' and upassword = '{LoingModel.Password} ';";
            Console.WriteLine(sql);
            UserBean bean = loginCheck(sql);
            if (bean == null)
            {
                this.ErrorMessage = "用户名密码错误";
            }else
            {
                this.ErrorMessage = $"登录成功，你好{bean.uname}";
            }

        }

        private UserBean loginCheck(string sql)
        {
            UserBean login = new UserBean();
            dataBase.cmd = new MySqlCommand(sql, dataBase.conn);
            dataBase.dr = dataBase.cmd.ExecuteReader();
           
            while (dataBase.dr.Read()) {

                login.uname = dataBase.dr[1].ToString();
                login.upassword = dataBase.dr[2].ToString();

            }

            return login;
        }

    }
}
