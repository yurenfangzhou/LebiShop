﻿

<%@ Page Language="C#" AutoEventWireup="true" Inherits="Shop.Bussiness.ShopPage" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<% LoadPage("jia5255@163.com_9",1,"tcn",""); %>

<style type="text/css" media="screen">
    html, body, div, span, applet, object, iframe, h1, h2, h3, h4, h5, h6, p, blockquote, pre, a, abbr, acronym, address, big, cite, code, del, dfn, em, img, ins, kbd, q, s, samp, small, strike, strong, sub, sup, tt, var, dd, dl, dt, li, ol, ul, fieldset, label, legend, table, caption, tbody, tfoot, thead, tr, th, td, form
    {
        margin: 0;
        padding: 0;
    }
    body
    {
        font: 12px/1.5em Tahoma, Verdana, Simsun, Microsoft YaHei, Arial Unicode MS, Mingliu, Arial, Helvetica;
        background-color: #fff;
    }
    ul, ol
    {
        list-style-type: none;
    }
    #wrap
    {
        text-align: left;
        width: 978px;
        height: 255px;
        zoom: 1;
        overflow: hidden;
        border: 1px solid #cccccc;
        background-color: #f7f7f7;
    }
    #wrap:after
    {
        clear: both;
        content: "";
        display: block;
        height: 0;
        line-height: 0;
        visibility: hidden;
    }
    #wrap dl
    {
        float: left;
        width: 194px;
        height: 120px;
        line-height: normal;
        border-right: 1px dotted #cccccc;
    }
    #wrap dl.n
    {
        border-right: none;
    }
    #wrap dt
    {
        font-weight: 700;
        color: #595959;
        padding: 7px 0 0 9px;
    }
    #wrap dt a:link, #wrap dt a:visited
    {
        color: #595959;
        text-decoration: none;
    }
    #wrap dt a:hover, #wrap dt a:active
    {
        color: #595959;
        text-decoration: none;
        background-color: #ebebeb;
    }
    #wrap dd
    {
        margin-bottom: 5px;
    }
    #wrap li
    {
        float: left;
        width: 48%;
        height: 19px;
        vertical-align: bottom;
    }
    #wrap li a
    {
        display: block;
        vertical-align: bottom;
        padding: 2px 0 1px 9px;
    }
    #wrap li a:link, #wrap li a:visited
    {
        color: #014ccc;
        text-decoration: none;
    }
    #wrap li a:hover, #wrap li a:active
    {
        color: #ff4e00;
        text-decoration: none;
        background-color: #ebebeb;
    }
    @media screen and (-webkit-min-device-pixel-ratio:0)
    {
        body
        {
            font-family: Microsoft YaHei;
        }
    }
</style>

<div id="wrap">
    <% 
        foreach(Lebi_Pro_Type c0 in EX_Product.Types(0)){
    %>
    <dl>
        <dt><a href="<%=URL("P_ProductCategory","id="+c0.id+"") %>" target="_blank">
            <%=Lang(c0.Name) %></a></dt>
        <dd>
            <ul>
                <% 
                    foreach(Lebi_Pro_Type c1 in EX_Product.Types(c0.id)){
                %>
                <li><a target="_blank" href="<%=URL("P_ProductCategory","id="+c1.id+"") %>">
                    <%=Lang(c1.Name) %></a></li>
                <%} %>
            </ul>
        </dd>
    </dl>
    <%} %>
</div>
