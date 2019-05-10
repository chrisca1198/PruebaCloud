<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="informativo.ascx.cs" Inherits="Cote_Cloud.User.informativo" %>
<link href="../css/sweetalert.css" rel="stylesheet" />
<div class="sweet-overlay" tabindex="-1" style="opacity: 1.04; display: block;">
    <div class="sweet-alert showSweetAlert visible" data-custom-class="" data-has-cancel-button="false"
        data-has-confirm-button="true" data-allow-outside-click="false" data-has-done-function="false"
        data-animation="pop" data-timer="null" style="display: block; margin-top: -115px;">
        <div class="sa-icon sa-error" style="display: none;">
            <span class="sa-x-mark">
                <span class="sa-line sa-left"></span>
                <span class="sa-line sa-right"></span>
            </span>
        </div>
        <div class="sa-icon sa-warning" style="display: none;">
            <span class="sa-body"></span>
            <span class="sa-dot"></span>
        </div>
        <div class="sa-icon sa-info" style="display: none;"></div>
        <div class="sa-icon sa-success" style="display: none;">
            <span class="sa-line sa-tip"></span>
            <span class="sa-line sa-long"></span>

            <div class="sa-placeholder"></div>
            <div class="sa-fix"></div>
        </div>
        <div class="sa-icon sa-custom" style="display: none;"></div>
        <h2>
            <asp:Label ID="lblEnca" runat="server" Text="lblEnca"></asp:Label></h2>
        <p style="display: block;">
            <asp:Label ID="lblMensaje" runat="server" Text="lblMensaje"></asp:Label>
        </p>
        <fieldset>
            <input type="text" tabindex="3" placeholder="">
            <div class="sa-input-error"></div>
        </fieldset>
        <div class="sa-error-container">
            <div class="icon">!</div>
            <p>Not valid!</p>
        </div>
        <div class="sa-button-container">
            <button class="cancel" tabindex="2" style="display: none; box-shadow: none;">Cancel</button>
            <div class="sa-confirm-button-container">
                <asp:Button ID="btnOk" runat="server" Text="OK"
                    class="confirm" TabIndex="1" Style="display: inline-block; background-color: rgb(140, 212, 245); box-shadow: rgba(140, 212, 245, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset;"
                    OnClick="btnOk_Click" />
                <div class="la-ball-fall">
                    <div></div>
                    <div></div>
                    <div></div>
                </div>
            </div>
        </div>
    </div>
</div>
