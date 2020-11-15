﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MessageBoxUsc_Message : System.Web.UI.UserControl
{
    public enum enmAnswer
    {
        OK = 0,
        Cancel = 1
    }
    public enum enmMessageType
    {
        Error = 0,
        Success = 1,
        Attention = 2,
        Info = 3
    }

    public class MsgBoxEventArgs : System.EventArgs
    {
        public enmAnswer Answer;
        public string Args;

        public MsgBoxEventArgs(enmAnswer answer, string args)
        {
            Answer = answer;
            Args = args;
        }
    }

    public delegate void MsgBoxEventHandler(object sender, MsgBoxEventArgs e);
    public event MsgBoxEventHandler MsgBoxAnswered;

    public string Args
    {
        get
        {
            if (ViewState["Args"] == null)
                ViewState["Args"] = "";

            return (Convert.ToString(ViewState["Args"]));
        }
        set
        {
            ViewState["Args"] = value;
        }
    }

    public class Message
    {
        public Message(string messageText, enmMessageType messageType)
        {
            _messageText = messageText;
            _messageType = messageType;
        }

        private enmMessageType _messageType = enmMessageType.Info;
        private string _messageText = "";

        public enmMessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

    }

    protected int MessageNumber
    {
        get
        {
            return Messages.Count;
        }
    }

    private List<Message> Messages = new List<Message>();

    public void AddMessage(string msgText, enmMessageType type)
    {
        Messages.Add(new Message(msgText, type));

        Args = "";
        btnPostCancel.Visible = false;
        btnPostOK.Visible = false;
        btnOK.Visible = true;
    }

    public void AddMessage(string msgText, enmMessageType type, bool postPage, bool showCancelButton, string args)
    {
        //Messages.Add(new Message(msgText, type));

        //if (!string.IsNullOrEmpty(args))
        //    Args = args;

        //btnPostCancel.Visible = showCancelButton;
        //btnPostOK.Visible = postPage;
        //btnOK.Visible = !postPage;
    }

    protected override void OnPreRender(EventArgs e)
    {
        //base.OnPreRender(e);

        ////if (btnOK.Visible == false)
        ////    mpeMsg.OkControlID = "btnD2";
        ////else
        //  mpeMsg.OkControlID = "btnOK";

        //if (Messages.Count > 0)
        //{
        //    btnOK.Focus();
        //    grvMsg.DataSource = Messages;
        //    grvMsg.DataBind();

        //    mpeMsg.Show();
        //    udpMsj.Update();
        //}
        //else
        //{
        //    grvMsg.DataBind();
        //    udpMsj.Update();
        //}
        //if (this.Parent.GetType() == typeof(UpdatePanel))
        //{
        //    UpdatePanel containerUpdatepanel = this.Parent as UpdatePanel;

        //    containerUpdatepanel.Update();
        //}
    }

    protected void btnPostOK_Click(object sender, EventArgs e)
    {
        if (MsgBoxAnswered != null)
        {
            MsgBoxAnswered(this, new MsgBoxEventArgs(enmAnswer.OK, Args)); 
           
            
            Args = "";
        }
    }

    protected void btnPostCancel_Click(object sender, EventArgs e)
    {
        if (MsgBoxAnswered != null)
        {
            MsgBoxAnswered(this, new MsgBoxEventArgs(enmAnswer.Cancel, Args));
           
            Args = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
