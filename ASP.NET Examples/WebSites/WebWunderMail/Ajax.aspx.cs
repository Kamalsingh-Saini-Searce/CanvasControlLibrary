﻿/*
    Canvas Control Library Copyright 2012
    Created by Akshay Srinivasan [akshay.srin@gmail.com]
    This javascript code is provided as is with no warranty implied.
    Akshay Srinivasan are not liable or responsible for any consequence of 
    using this code in your applications.
    You are free to use it and/or change it for both commercial and non-commercial
    applications as long as you give credit to Akshay Srinivasan the creator 
    of this code.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Reflection;
using System.Collections;

using MailBee;
using MailBee.Mime;
using MailBee.Security;
using MailBee.SmtpMail;

using MailBee.DnsMX;
using MailBee.Pop3Mail;
using MailBee.ImapMail;

public partial class Ajax : System.Web.UI.Page
{
    CanvasControlLibrary ccl;
    ArrayList parameters = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        ccl = new CanvasControlLibrary(Request.InputStream);
        ccl.InvokeServerSideFunction(this.Page);
        ccl.SendVars(Response.OutputStream, parameters);
    }

    protected override void Render(HtmlTextWriter writer)
    {
    }

    public void getFolders(string canvasid, int windowid)
    {
        Imap imp = new Imap();
        imp.Connect(((ArrayList)ccl.InputParams[0])[2].ToString());
        imp.Login(((ArrayList)ccl.InputParams[0])[0].ToString(), ((ArrayList)ccl.InputParams[0])[1].ToString());
        FolderCollection fc = imp.DownloadFolders();
        foreach (Folder fcx in fc)
        {
            parameters.Add(fcx.Name);
        }
        imp.Disconnect();
    }
}