﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="VDM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <title>MACHINE CONSOLE</title>
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" href="../Styles/webfont.css">
  <link rel="stylesheet" href="../styles/climacons-font.css">
  <link rel="stylesheet" href="../vendor/bootstrap/dist/css/bootstrap.css">
  <link rel="stylesheet" href="../styles/font-awesome.css">
  <link rel="stylesheet" href="../styles/card.css">
  <link rel="stylesheet" href="../styles/sli.css">
  <link rel="stylesheet" href="../styles/animate.css">
  <link rel="stylesheet" href="../styles/app.css">
  <link rel="stylesheet" href="../styles/app.skins.css">

</head>
<body class="page-loading">
 <form id="form1" runat="server">
  <!-- page loading spinner -->
  <!--<div class="pageload">
    <div class="pageload-inner">
      <div class="sk-rotating-plane"></div>
    </div>
  </div>-->
  <!-- /page loading spinner -->
  <div class="app signin usersession">
    <div class="session-wrapper">
      <div class="page-height-o row-equal align-middle">
        <div class="column">
          <div class="card bg-white no-border">
            
              <asp:Panel CssClass="form-layout" ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                <div class="text-center m-b">
                  <h4 class="text-uppercase">VENDING Management CONSOLE</h4>
                    <p>
                        <img id="imgLogo" src="../images/Icon/koisk_ab.png" style="width: 10%;">
                    </p>
                  <p>Sign In to MACHINE CONSOLE</p>
                </div>
                <div class="form-inputs">
                  <label class="text-uppercase">User Name</label>
                  <asp:TextBox ID="txtUser" runat="server" CssClass="form-control input-lg" placeholder="User Name"></asp:TextBox>
                  <label class="text-uppercase">Password</label>
                  <asp:TextBox ID="txtPass" runat="server" class="form-control input-lg" TextMode="Password" placeholder="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-danger btn-block btn-lg " Text="Sign In" />
                <div style="text-align:center;">
                    <asp:Label ID="lblError" runat="server" CssClass="text-center m-b" style=" width:100%; color:red;">
                        Invalid admin username and password
                    </asp:Label>
                </div>
                  
              </asp:Panel>
        
          </div>
        </div>
      </div>
    </div>
    <!-- bottom footer -->
    <footer class="session-footer">
      <nav class="footer-right">
        <ul class="nav">
          <li>
            <a href="javascript:;">Support</a>
          </li>
          <li>
            <a href="javascript:;" class="scroll-up">
              <i class="fa fa-angle-up"></i>
            </a>
          </li>
        </ul>
      </nav>
      <nav class="footer-left hidden-xs">
        <ul class="nav">

        </ul>
      </nav>
    </footer>
    <!-- /bottom footer -->
  </div>

     <!-- build:js({.tmp,app}) scripts/app.min.js -->
    <script src="../scripts/helpers/modernizr.js" type="text/javascript"></script>
    <script src="../vendor/jquery/dist/jquery.js" type="text/javascript"></script>
    <script src="../vendor/bootstrap/dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../vendor/fastclick/lib/fastclick.js" type="text/javascript"></script>
    <script src="../vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.js" type="text/javascript"></script>
    <script src="../scripts/helpers/smartresize.js" type="text/javascript"></script>
    <script src="../scripts/jquery.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/constants.js" type="text/javascript"></script>
    <script src="../scripts/main.js" type="text/javascript"></script>

  <!-- build:js({.tmp,app}) scripts/app.min.js -->
  <script src="../vendor/chosen_v1.4.0/chosen.jquery.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/checkbo/src/0.1.4/js/checkBo.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/intl-tel-input//build/js/intlTelInput.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/jquery.tagsinput/src/jquery.tagsinput.js"></script>
    <script src="../vendor/moment/min/moment.min.js"></script>
    <script src="../vendor/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script src="../vendor/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../vendor/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../vendor/clockpicker/dist/jquery-clockpicker.min.js"></script>
    <script src="../vendor/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"></script>  
    <script src="../vendor/moment/min/moment.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/select2/dist/js/select2.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/selectize/dist/js/standalone/selectize.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/jquery-labelauty/source/jquery-labelauty.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/bootstrap-maxlength/bootstrap-maxlength.min.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/typeahead.js/dist/typeahead.bundle.js" type="text/javascript" language="javascript"></script>
    <script src="../vendor/multiselect/js/jquery.multi-select.js" type="text/javascript" language="javascript"></script>
  <!-- endbuild -->

</form>
</body>
</html>
