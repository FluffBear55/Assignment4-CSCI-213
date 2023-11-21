<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment_4._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Karate School</h1>
            <p class="lead">Welcome to the website for our Karate School</p>
        </section>
    <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Members</h2>
                <p>
                    Login to see currently enrolled classes or due payments</p>
                <p>
                    <a class="btn btn-default" href="StudentInfo/StudentInfo.aspx">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2 id="librariesTitle">Instructors</h2>
                <p>
                    Sign in to see current members</p>
                <p>
                    <a class="btn btn-default" href="InstructorInfo/InstructorInfo.aspx">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                <h2 id="hostingTitle">Learn More</h2>
                <p>
                    To contact us, click the link below</p>
                <p>
                    <a class="btn btn-default" href="Contact.aspx">Learn more &raquo;</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
