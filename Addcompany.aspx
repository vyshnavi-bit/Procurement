<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Addcompany.aspx.cs" Inherits="Addcompany" Title="OnlineMilkTest|Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="1000" colspan="2"><br />
                <p style="line-height: 100%">
                    <font class="Highlight">&nbsp;&nbsp;Register</font>
                </p>
            </td>
        </tr>
        <tr>
            <td width="1000" height="3px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="1000" class="line" height="1px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="1000" height="7" colspan="2">
            </td>
        </tr>
        
        <tr>
            <td width="1000" colspan="2">
             </td>
        </tr>
    </table></div>
    
            <div align="center">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
<table border="0" width="1000" id="table3" cellpadding="2">

                                   
                                    <tr>
                                        <td width="30%"></td>
                                       
                                         <td width="10%" align="left" class="fontt">
                                            &nbsp;ID:</td>
                                        <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtcompanyid" runat="server" BackColor="Silver" 
                                                ReadOnly="True" Width="73px" CssClass="txtStyle"></asp:TextBox></td>
                                                 <td width="30%"></td>
                                    </tr>
                                    <tr>
                                        <td width="30%"></td>
                                        
                                        <td width="15%" align="left" class="fontt">
                                                                                        Userid:</td>
                                         <td width="25%" align="left" class="fontt">
                                            <asp:TextBox ID="txtcompanyuserid" runat="server" BackColor="Silver" 
                                                ReadOnly="True" Width="138px"></asp:TextBox></td>
                                                <td width="30%"></td>
                                    </tr>
                                    <tr>
                                    <td width="30%"></td>
                                         <td width="10%" align="left" class="fontt">
                                             Password:</td>
                                        <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtPassword" runat="server" Width="186px" TabIndex="9"></asp:TextBox>
                                           
                                        </td>
                                         <td width="30%"></td>
                                    </tr>
                                    <%--<tr>
                                     <td width="40%"></td>
                                       
                                        <td width="10%" align="left" class="fontt">
                                            Name</td>
                                        <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtcompanyname" runat="server" Width="187px" 
                                                MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcompanyname"
                                                ErrorMessage="* Required"></asp:RequiredFieldValidator></td>  
                                                <td width="20%"></td>
                                    </tr>--%>
									<tr>
									  <td width="30%"></td>
										<td width="10%" align="left" class="fontt">First name:</td>
										<td width="30%" align="left" class="fontt"><asp:TextBox ID="txtfirstname" 
                                                runat="server" Width="188px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfirstname"
                                                ErrorMessage="* Required" ></asp:RequiredFieldValidator></td> 
                                               <td width="30%"></td>
									</tr>
									<tr>
										 <td width="30%"></td>
										<td width="10%" align="left" class="fontt">Last name:</td>
										<td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtlastname" 
                                                runat="server" Width="184px" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtlastname"
                                                ErrorMessage="* Required" ></asp:RequiredFieldValidator></td>
                                                 <td width="30%"></td>
									</tr>
									<tr>
									<td width="30%"></td>
										<td width="8%" align="left" class="fontt">Email id:</td>
										<td width="32%" align="left" class="fontt">
                                            <asp:TextBox ID="txtEmailID" 
                                                runat="server" Width="187px" MaxLength="50" TabIndex="2"></asp:TextBox>
                                                
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailID"
                                                ErrorMessage="*Invalid EmailID" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                 <td width="30%"></td>
									</tr>
                                    <tr>
                                      <td width="30%"></td>
                                        <td width="10%" align="left" class="fontt">
                                            Fax:</td>
                                        <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtFax" runat="server" Width="187px" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Required" ControlToValidate="txtFax"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="30%"></td>
                                    </tr>
                                    <tr>
                                    <td width="30%"></td>
                                        
                                       <td width="10%" align="left" class="fontt">
                                            Address:</td>
                                       <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Width="187px" 
                                                MaxLength="250" TabIndex="4"></asp:TextBox></td>
                                            <td width="30%"></td>
                                    </tr>
									
                                    <tr>
                                     <td width="30%"></td>
                                        <td width="10%" align="left" class="fontt">
                                            Tele (Off):</td>
                                       <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="TxtTeleOff" runat="server" Width="187px" MaxLength="50" 
                                                TabIndex="5"></asp:TextBox></td>
                                           <td width="30%"></td>
                                    </tr>
                                    <tr>
                                      <td width="30%"></td>
                                      <td width="10%" align="left" class="fontt">
                                            Mobile:</td>
                                       <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="TxtMobile" runat="server" Width="187px" MaxLength="50" 
                                                TabIndex="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtMobile"
                                                ErrorMessage="* Required"></asp:RequiredFieldValidator></td>
                                                <td width="30%"></td>
                                    </tr>
                                    <tr>
                                    <td width="30%"></td>
                                        <td width="10%" align="left" class="fontt">
                                            City:</td>
                                       <td width="30%" align="left" class="fontt">
                                            <asp:TextBox ID="txtcity" runat="server" Width="188px" TabIndex="7"></asp:TextBox>
                                        </td>
                                     <td width="30%"></td>
                                    </tr>
                                    <tr>
                                     <td width="30%"></td>
                                       <td width="10%" align="left" class="fontt">
                                            Country:</td>
                                       <td width="30%" align="left" class="fontt">
                                           
                                            <asp:DropDownList ID="DdlistCountry" runat="server" Height="20px" Width="186px" 
                                                TabIndex="8">
                                             <asp:ListItem>--Select--</asp:ListItem>
            <asp:ListItem>Afghanistan</asp:ListItem>
            <asp:ListItem>Algeria</asp:ListItem>
            <asp:ListItem>Albania</asp:ListItem>
            <asp:ListItem>American Samoa</asp:ListItem>
            <asp:ListItem>Andorra</asp:ListItem>
            <asp:ListItem>Angola</asp:ListItem>
            <asp:ListItem>Anguilla</asp:ListItem>
            <asp:ListItem>Antarctica</asp:ListItem>
            <asp:ListItem>Antigua and Barbuda</asp:ListItem>
            <asp:ListItem>Argentina</asp:ListItem>
            <asp:ListItem>Armenia</asp:ListItem>
            <asp:ListItem>Aruba</asp:ListItem>
            <asp:ListItem>Australia</asp:ListItem>
            <asp:ListItem>Austria</asp:ListItem>
            <asp:ListItem>Azerbaijan</asp:ListItem>
            <asp:ListItem>Bahamas</asp:ListItem>
            <asp:ListItem>Bahrain</asp:ListItem>
            <asp:ListItem>Bangladesh</asp:ListItem>
            <asp:ListItem>Barbados</asp:ListItem>
            <asp:ListItem>Belarus</asp:ListItem>
             <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
							 <asp:ListItem Value="Belize">Belize</asp:ListItem>
							 <asp:ListItem Value="Benin">Benin</asp:ListItem>
							 <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
							 <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
							 <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
							 <asp:ListItem Value="Bosnia and Herzegovina">Bosnia and Herzegovina</asp:ListItem>
							 <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
							 <asp:ListItem Value="Bouvet Island">Bouvet Island</asp:ListItem>
							 <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
							 <asp:ListItem Value="British Indian Ocean T">British Indian Ocean T</asp:ListItem>
							 <asp:ListItem Value="Brunei Darussalam">Brunei Darussalam</asp:ListItem>
							 <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
							 <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
							 <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
							 <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
							 <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
							 <asp:ListItem Value="Canada">Canada</asp:ListItem>
							 <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
							 <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
							 <asp:ListItem Value="Central African Republ">Central African Republ</asp:ListItem>
							 <asp:ListItem Value="Chad">Chad</asp:ListItem>
							 <asp:ListItem Value="Chile">Chile</asp:ListItem>
							 <asp:ListItem Value="China">China</asp:ListItem>
							 <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
							 <asp:ListItem Value="Cocos (Keeling) Island">Cocos (Keeling) Island</asp:ListItem>
							 <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
							 <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
							 <asp:ListItem Value="Congo">Congo</asp:ListItem>
							 <asp:ListItem Value="Congo, The Democratic">Congo, The Democratic</asp:ListItem>
							 <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
							 <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
							 <asp:ListItem Value="Cote D'Ivoire">Cote D'Ivoire</asp:ListItem>
							 <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
							 <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
							 <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
							 <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
							 <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
							 <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
							 <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
							 <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
							 <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
							 <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
							 <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
							 <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
							 <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
							 <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
							 <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
							 <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
							 <asp:ListItem Value="Falkland Islands(Malvi)">Falkland Islands(Malvi)</asp:ListItem>
							 <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
							 <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
																	<asp:ListItem Value="Finland">Finland</asp:ListItem>
																	<asp:ListItem Value="France">France</asp:ListItem>
																	<asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
																	<asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
																	<asp:ListItem Value="French Southern Territ">French Southern Territ</asp:ListItem>
																	<asp:ListItem Value="Gabon">Gabon</asp:ListItem>
																	<asp:ListItem Value="Gambia">Gambia</asp:ListItem>
																	<asp:ListItem Value="Georgia">Georgia</asp:ListItem>
																	<asp:ListItem Value="Germany">Germany</asp:ListItem>
																	<asp:ListItem Value="Ghana">Ghana</asp:ListItem>
																	<asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
																	<asp:ListItem Value="Greece">Greece</asp:ListItem>
																	<asp:ListItem Value="Greenland">Greenland</asp:ListItem>
																	<asp:ListItem Value="Grenada">Grenada</asp:ListItem>
																	<asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
																	<asp:ListItem Value="Guam">Guam</asp:ListItem>
																	<asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
																	<asp:ListItem Value="Guinea">Guinea</asp:ListItem>
																	<asp:ListItem Value="Guinea-Bissau">Guinea-Bissau</asp:ListItem>
																	<asp:ListItem Value="Guyana">Guyana</asp:ListItem>
																	<asp:ListItem Value="Haiti">Haiti</asp:ListItem>
																	<asp:ListItem Value="Heard and McDonald Isl">Heard and McDonald Isl</asp:ListItem>
																	<asp:ListItem Value="Holy See (Vatican City)">Holy See (Vatican City)</asp:ListItem>
																	<asp:ListItem Value="Honduras">Honduras</asp:ListItem>
																	<asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
																	<asp:ListItem Value="Hungary">Hungary</asp:ListItem>
																	<asp:ListItem Value="Iceland">Iceland</asp:ListItem>
																	<asp:ListItem Value="India">India</asp:ListItem>
																	<asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
																	<asp:ListItem Value="Iran, Islamic Republic">Iran, Islamic Republic</asp:ListItem>
																	<asp:ListItem Value="Iraq">Iraq</asp:ListItem>
																	<asp:ListItem Value="Ireland">Ireland</asp:ListItem>
																	<asp:ListItem Value="Israel">Israel</asp:ListItem>
																	<asp:ListItem Value="Italy">Italy</asp:ListItem>
																	<asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
																	<asp:ListItem Value="Japan">Japan</asp:ListItem>
																	<asp:ListItem Value="Jordan">Jordan</asp:ListItem>
																	<asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
																	<asp:ListItem Value="Kenya">Kenya</asp:ListItem>
																	<asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
																	<asp:ListItem Value="Korea, Democratic Peop">Korea, Democratic Peop</asp:ListItem>
																	<asp:ListItem Value="Korea, Rebublic of">Korea, Rebublic of</asp:ListItem>
																	<asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
																	<asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
																	<asp:ListItem Value="Lao People's Democrati">Lao People's Democrati</asp:ListItem>
																	<asp:ListItem Value="Latvia">Latvia</asp:ListItem>
																	<asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
																	<asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
																	<asp:ListItem Value="Liberia">Liberia</asp:ListItem>
																	<asp:ListItem Value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</asp:ListItem>
																	<asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
																	<asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
																	<asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
																	<asp:ListItem Value="Macao">Macao</asp:ListItem>
																	<asp:ListItem Value="Macedonia, The Former">Macedonia, The Former</asp:ListItem>
																	<asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
																	<asp:ListItem Value="Malawi">Malawi</asp:ListItem>
																	<asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
																	<asp:ListItem Value="Maldives">Maldives</asp:ListItem>
																	<asp:ListItem Value="Mali">Mali</asp:ListItem>
																	<asp:ListItem Value="Malta">Malta</asp:ListItem>
																	<asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
																	<asp:ListItem Value="Martinique">Martinique</asp:ListItem>
																	<asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
																	<asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
																	<asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
																	<asp:ListItem Value="Mexico">Mexico</asp:ListItem>
																	<asp:ListItem Value="Micronesia, Federated">Micronesia, Federated</asp:ListItem>
																	<asp:ListItem Value="Moldova, Republic of">Moldova, Republic of</asp:ListItem>
																	<asp:ListItem Value="Monaco">Monaco</asp:ListItem>
																	<asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
																	<asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
																	<asp:ListItem Value="Morocco">Morocco</asp:ListItem>
																	<asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
																	<asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
																	<asp:ListItem Value="Namibia">Namibia</asp:ListItem>
																	<asp:ListItem Value="Nauru">Nauru</asp:ListItem>
																	<asp:ListItem Value="Nepal">Nepal</asp:ListItem>
																	<asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
																	<asp:ListItem Value="Netherlands Antilles">Netherlands Antilles</asp:ListItem>
																	<asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
																	<asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
																	<asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
																	<asp:ListItem Value="Niger">Niger</asp:ListItem>
																	<asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
																	<asp:ListItem Value="Niue">Niue</asp:ListItem>
																	<asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
																	<asp:ListItem Value="Northern Mariana Islan">Northern Mariana Islan</asp:ListItem>
																	<asp:ListItem Value="Norway">Norway</asp:ListItem>
																	<asp:ListItem Value="Oman">Oman</asp:ListItem>
																	<asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
																	<asp:ListItem Value="Palau">Palau</asp:ListItem>
																	<asp:ListItem Value="Palestinian Territory,">Palestinian Territory,</asp:ListItem>
																	<asp:ListItem Value="Panama">Panama</asp:ListItem>
																	<asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
																	<asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
																	<asp:ListItem Value="Peru">Peru</asp:ListItem>
						<asp:ListItem Value="Philippines">Philippines</asp:ListItem>
						<asp:ListItem Value="Pitcairn">Pitcairn</asp:ListItem>
						<asp:ListItem Value="Poland">Poland</asp:ListItem>
						<asp:ListItem Value="Portugal">Portugal</asp:ListItem>
						<asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
						<asp:ListItem Value="Qatar">Qatar</asp:ListItem>
						<asp:ListItem Value="Reunion">Reunion</asp:ListItem>
						<asp:ListItem Value="Romania">Romania</asp:ListItem>
						<asp:ListItem Value="Russian Federation">Russian Federation</asp:ListItem>
						<asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
						<asp:ListItem Value="S. Georgia and S. Sand">S. Georgia and S. Sand</asp:ListItem>
						<asp:ListItem Value="Saint Helena">Saint Helena</asp:ListItem>
						<asp:ListItem Value="Saint Kitts and Nevis">Saint Kitts and Nevis</asp:ListItem>
						<asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
						<asp:ListItem Value="Saint Pierre and Mique">Saint Pierre and Mique</asp:ListItem>
						<asp:ListItem Value="Saint Vincent and Gren">Saint Vincent and Gren</asp:ListItem>
						<asp:ListItem Value="Samoa">Samoa</asp:ListItem>
						<asp:ListItem Value="San Marino">San Marino</asp:ListItem>
						<asp:ListItem Value="Sao Tome and Principe">Sao Tome and Principe</asp:ListItem>
						<asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
						<asp:ListItem Value="Senegal">Senegal</asp:ListItem>
						<asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
						<asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
						<asp:ListItem Value="Singapore">Singapore</asp:ListItem>
						<asp:ListItem Value="Slovakia">Slovakia</asp:ListItem>
						<asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
						<asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
						<asp:ListItem Value="Somalia">Somalia</asp:ListItem>
						<asp:ListItem Value="South Africa">South Africa</asp:ListItem>
						<asp:ListItem Value="Spain">Spain</asp:ListItem>
						<asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
						<asp:ListItem Value="Sudan">Sudan</asp:ListItem>
						<asp:ListItem Value="Suriname">Suriname</asp:ListItem>
						<asp:ListItem Value="Svalbard and Jan Mayen">Svalbard and Jan Mayen</asp:ListItem>
						<asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
						<asp:ListItem Value="Sweden">Sweden</asp:ListItem>
						<asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
						<asp:ListItem Value="Syrian Arab Republic">Syrian Arab Republic</asp:ListItem>
						<asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
						<asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
						<asp:ListItem Value="Tanzania, United Repub">Tanzania, United Repub</asp:ListItem>
						<asp:ListItem Value="Thailand">Thailand</asp:ListItem>
						<asp:ListItem Value="Togo">Togo</asp:ListItem>
						<asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
						<asp:ListItem Value="Tonga">Tonga</asp:ListItem>
						<asp:ListItem Value="Trinidad and Tobago">Trinidad and Tobago</asp:ListItem>
						<asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
						<asp:ListItem Value="Turkey">Turkey</asp:ListItem>
						<asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
						<asp:ListItem Value="Turks and Caicos Island">Turks and Caicos Island</asp:ListItem>
						<asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
						<asp:ListItem Value="Uganda">Uganda</asp:ListItem>
						<asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>																	
						<asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
						<asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
						<asp:ListItem Value="United States">United States</asp:ListItem>
						<asp:ListItem Value="United States Minor Ou">United States Minor Ou</asp:ListItem>
						<asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
						<asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
						<asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
						<asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
						<asp:ListItem Value="Viet Nam">Viet Nam</asp:ListItem>
						<asp:ListItem Value="Virgin Islands, British">Virgin Islands, British</asp:ListItem>
						<asp:ListItem Value="Virgin Islands, U.S.">Virgin Islands, U.S.</asp:ListItem>
						<asp:ListItem Value="Wallis and Futuna Isla">Wallis and Futuna Isla</asp:ListItem>
						<asp:ListItem Value="Western Sahara">Western Sahara</asp:ListItem>
						<asp:ListItem Value="Yugoslavia">Yugoslavia</asp:ListItem>
						<asp:ListItem Value="Zambia">Zambia</asp:ListItem>
						<asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </td>
                                       <td width="30%"></td>
                                    </tr>
                                     <%--<tr>
                                      <td width="40%"></td>
                                        <td width="10%" align="left" class="fontt">
                                            No Of Users:</td>
                                         <td width="30%" align="left" class="fontt">
                                           
                                            <asp:TextBox ID="txtUsers" runat="server" Width="186px"></asp:TextBox>
                                           
                                        </td>
                                         <td width="20%"></td>
                                    </tr>--%>
                                     
                                     <tr>
                                       <td width="30%"></td>
                                      <td width="10%" align="left" class="fontt">
                                            Date:</td>
                                       <td width="30%" align="left" class="fontt">
                                           
                                             <asp:TextBox ID="txt_fromdate" runat="server" Enabled="False" TabIndex="10"></asp:TextBox>
                                           <asp:ImageButton ID="popupcal" runat="server" ImageUrl="~/calendar.gif" Height="20px" />
                                           <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fromdate" PopupButtonID="popupcal" TodaysDateFormat="dd/MM/yyyy" PopupPosition="TopRight">
                                           </asp:CalendarExtender>
                                        </td>
                                         <td width="30%"></td>
                                    </tr>
									<tr>
										
										 <td width="30%"></td>
										  <td width="10%" align="left" class="fontt"></td>
										  <td width="30%" align="left" class="fontt">&nbsp;<asp:Button ID="BtnSubmit" 
                                                  runat="server" Text="Create" OnClick="BtnSubmit_Click" BackColor="#6F696F" 
                                                  Font-Bold="False" ForeColor="White" Width="49px" TabIndex="11" />
                                           </td>
                                            <td width="30%"></td>
									</tr>
									
								</table>
	
								</div>
	
	   

 <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />



</asp:Content>

