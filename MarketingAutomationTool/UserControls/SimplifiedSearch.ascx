<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimplifiedSearch.ascx.cs" Inherits="MarketingAutomationTool.UserControls.SimplifiedSearch" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<div class="container-fluid">
                            <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
	                              <button type="button" class="close" data-dismiss="alert" aria-label="Close">
        	                            <span aria-hidden="true">&times;</span>
                                   </button>
                             </div>
							<ul class="nav nav-tabs" id="secondtab">
							  <li class="nav-item">
								<a class="nav-link active" href="#" id="lnkSearch" >Simplified Search</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link" href="#"  id="lnkResults">Search Results</a>
							  </li>
							</ul>
							<div class="tab-content">
							<div class="tab-pane active" id="tab_Search">
									<div class="form-horizontal">
										<div class="container">
										<div class="row">
												<div class="form-group form-group-sm col-sm-12">
												<div class="accordion" id="simplifiedSearch">
											  <div class="card">
												<div class="card-header" id="headingOne">
												  <h2 class="mb-0">
													<button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
													  Built-in Fields
													</button>
												  </h2>
												</div>

												<div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#simplifiedSearch">
												  <div class="card-body" id="BuiltInFields">
													<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupfirstname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">First Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_firstname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtfirstname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_firstname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupfirstname" class="btn btn-secondary" onclick="AddField('firstname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_firstname">

                                                                    
                                                                </div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupmiddlename">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Middle Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_middlename" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtmiddlename" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_middlename" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupmiddlename" class="btn btn-secondary" onclick="AddField('middlename')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_middlename">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgrouplastname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Last Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_lastname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtlastname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_lastname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgrouplastname" class="btn btn-secondary" onclick="AddField('lastname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_lastname">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupemail">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Email</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_email" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtemail" class="form-control" maxlength="250"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_email" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupemail" class="btn btn-secondary" onclick="AddField('email')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_email">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcompanyname">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Company Name</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_companyname" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtcompanyname" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_companyname" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcompanyname" class="btn btn-secondary" onclick="AddField('companyname')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_companyname">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupposition">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Position</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_position" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtposition" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_position" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupposition" class="btn btn-secondary" onclick="AddField('position')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_position">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupwebsite" >
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Website</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_website" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtwebsite" class="form-control" maxlength="250"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_website" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupwebsite" class="btn btn-secondary" onclick="AddField('website')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_website">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupmobile">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Mobile</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_mobile" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtmobile" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_mobile" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupmobile" class="btn btn-secondary" onclick="AddField('mobile')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_mobile">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupphoneno">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Phone No</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_phoneno" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtphoneno" class="form-control" maxlength="100"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_phoneno" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupphoneno" class="btn btn-secondary" onclick="AddField('phoneno')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_phoneno">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupaddress">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Address</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_address" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtaddress" class="form-control" maxlength="501"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_address" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupaddress" class="btn btn-secondary" onclick="AddField('address')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_address">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcity">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">City</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_city" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtcity" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_city" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcity" class="btn btn-secondary" onclick="AddField('city')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_city">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupstate">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">State</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_state" class="form-control" onchange="operatoronchange(this,'text')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<input  type="text" id="txtstate" class="form-control" maxlength="50"/>																
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_state" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupstate" class="btn btn-secondary" onclick="AddField('state')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_state">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcountry">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Country</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_country" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcountry" class="form-control">
																			
																		</select>															
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_country" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcountry" class="btn btn-secondary" onclick="AddField('country')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_country">

                                                                    
                                                                </div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Type</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_contacttype" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcontacttype" class="form-control">
                                                                            <option value=""></option>
																			<option value="1">Lead</option>
																			<option value="2">Contact</option>
																		</select>															
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_contacttype" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12" id="dvgroupcontactstatus">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Status</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_contactstatus" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlcontactstatus" class="form-control">
                                                                            <option value="" ></option>
                                                                            <option value="1">Not Confirmed</option>
																			<option value="2">Confirmed</option>
																			<option value="3">Active</option>
																			<option value="4">Inactive</option>
																		</select>															
																	</div>
																	<div class="col-sm-2">
																		<select id="logicaloperator_contactstatus" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																	<div class="col-sm-1 pull-left">
																		<a href="#dvgroupcontactstatus" class="btn btn-secondary" onclick="AddField('contactstatus')"><i class="fas fa-plus-circle"></i></a>
																	</div>
																</div>
                                                                <div id="dv_addl_contactstatus">

                                                                    
                                                                </div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Gender</label>
																	</div>
																	<div class="col-sm-2">
																		<select  id="operator_gender" class="form-control" onchange="operatoronchange(this,'select')">
																			<option value="LIKE">LIKE</option>
                                                                            <option value="NOT LIKE">NOT LIKE</option>
                                                                            <option value="=">=</option>
                                                                            <option value="<>"><></option>
                                                                            <option value="IS NULL">IS NULL</option>
                                                                            <option value="IS NOT NULL">IS NOT NULL</option>
                                                                            <option value="<"><</option>
                                                                            <option value="<="><=</option>
                                                                            <option value=">">></option>
                                                                            <option value=">=">>=</option>
																		</select>
																	</div>
																	<div class="col-sm-4">
																		<select id="ddlgender" class="form-control">
																			<option></option>
																			<option>Male</option>
																			<option>Female</option>
																		</select>															
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_gender" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
														<div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Email Subscription</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlsubscribedtoemail" class="form-control" >
																				<option value=""></option>
																				<option value="0">Unsubscribed</option>
																				<option value="1">Subscribed</option>
																			</select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_subscribedtoemail" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Deleted</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlisdeleted" class="form-control">
																				<option value=""></option>
																				<option value="1">Yes</option>
																				<option value="0" selected>No</option>
																	    </select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_isdeleted" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
                                                      <div class="row">
															<div class="form-group form-group-sm col-sm-12">
																<div class="row">
																	<div class="col-sm-3 text-right">
																		<label class="col-form-label">Used for Testing</label>
																	</div>
																	<div class="col-sm-6">
																		<select id="ddlusefortesting" class="form-control">
																				<option value=""></option>
																				<option value="1">Yes</option>
																				<option value="0">No</option>
																			</select>														
																	</div>
                                                                    <div class="col-sm-2">
																		<select id="logicaloperator_usefortesting" class="form-control">
																			<option>AND</option>
																			<option>OR</option>
																		</select>
																	</div>
																</div>
															</div>
														</div>
												  </div>
												</div>
											  </div>
											  <div class="card">
												<div class="card-header" id="headingLists">
												  <h2 class="mb-0">
													<button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseLists" aria-expanded="false" aria-controls="collapseThree">
													  Lists
													</button>
												  </h2>
												</div>
												<div id="collapseLists" class="collapse" aria-labelledby="headingLists" data-parent="#simplifiedSearch">
												  <div class="card-body">
													<div class="form-horizontal">
														<div class="container">
															<div class="row">
																<div class="form-group form-group-sm col-sm-12" id="dvgroupcontactlist">
																	<div class="row">
																		<div class="col-sm-2 text-right">
																			<label class="col-form-label" >Select a List</label>
																		</div>
																		<div class="col-sm-7">
																			 <select id="ddlcontactlist"  class="form-control"></select>
																		</div>
																		<div class="col-sm-2">
																			  <select id="logicaloperator_contactlist" class="form-control">
																				<option>AND</option>
																				<option>OR</option>
																			</select>
																		</div>
																		<div class="col-sm-1 pull-left">
																			  <a href="#dvgroupcontactlist" class="btn btn-secondary" onclick="AddField('contactlist')"><i class="fas fa-plus-circle"></i></a>
																		</div>
																	</div>
                                                                     <div id="dv_addl_contactlist">

                                                                    
                                                                    </div>
																</div>
															</div>
														</div>
													</div>
												  </div>
												</div>
											  </div>
											</div>
												</div>
										</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary" OnClientClick="return GetSearchValues();" OnClick="btnSearch_Click" />
														</div>
													</div>
													</div>
												</div>
											</div>
											
										</div>
									</div>
								</div>
								<div class="tab-pane" id="tab_Results">
									<div class="form-horizontal">
										<div class="container">
											<div class="row">
												<div class="col-lg-12">
													<div class="alert alert-success searchcriteria" role="alert" runat="server" id="dvSearchCriteria">
													 
													</div>
												</div>
											</div>
                                            <div runat="server" id="dvIncludedLabel">
                                            <legend class="legendstyle">Included</legend>
                                            </div>
											<div class="row">
												<div class="col-lg-12">
                                                    <asp:GridView ID="grdIncluded" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdIncluded_RowCommand" OnRowDeleting="grdIncluded_RowDeleting" OnRowDataBound="grdIncluded_RowDataBound"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkExclude" runat="server" Text="EXCLUDE" CommandName="EXCLUDE" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lead ID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ID") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Facebook ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFacebookID" runat="server" Text='<%# Bind("FacebookID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete"  runat="server"  CommandName="DELETE" CommandArgument='<%# Bind("ContactID") %>' OnClientClick='<%# "return ConfirmDeleteLeadFromTable(" + Eval("IsDeletedNum")  + ");" %>' rel="tooltip" class="iconlink" data-placement="top" title="Delete"><i class="fas fa-trash-alt" id="trashicon"></i></asp:LinkButton>
                                                                    <a href="#" runat="server" id="lnkNoDelete" rel="tooltip" class="iconlink" data-placement="top" title="Deleted Record"><i class="fas fa-trash-alt" id="trashicondisabled" style="color:#CCC;"></i></a>
                                                                    <asp:HiddenField ID="hdnDeleted" runat="server" Value='<%# Bind("IsDeleted") %>' />                                                               
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
											</div>
											<br/>
											<div class="row" > 
												 <uc:Pager runat="server" id="Pager"></uc:Pager>
											</div>
                                            <div class="row" runat="server" id="dvExport">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-primary" OnClick="btnExport_Click"   />
														</div>
													</div>
													</div>
												</div>
											</div>
                                            <div class="row" runat="server" id="dvAddMembers">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
															<asp:Button ID="btnAddMembers" runat="server" Text="Add Members" class="btn btn-primary" OnClick="btnAddMembers_Click"  />
														</div>
													</div>
													</div>
												</div>
											</div>
                                            <br/>
                                            <div runat="server" id="dvExcluded">
                                            <div runat="server" id="dvExcludedLabel">
                                            <legend class="legendstyle">Excluded</legend>
                                            </div>
											<div class="row">
												<div class="col-lg-12">
                                                    <asp:GridView ID="grdExcluded" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdExcluded_RowCommand"   >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkInclude" runat="server" Text="INCLUDE" CommandName="INCLUDE" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lead ID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ID") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FirstName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="LastName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Facebook ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFacebookID" runat="server" Text='<%# Bind("FacebookID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
											</div>
											<br/>
											<div class="row" > 
												 <uc:Pager runat="server" id="PagerExcluded"></uc:Pager>
											</div>
                                                
                                            </div>
                                            
										</div>
									</div>
								</div>
							</div>
						</div>