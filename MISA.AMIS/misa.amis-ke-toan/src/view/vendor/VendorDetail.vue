<template>
  <div class="m-dialog" id="mDialog">
    <div class="dialog-info">
      <div class="dialog" id="dialog" ref="dialogDragable">
        <div class="tool">
          <div class="help-button"></div>
          <div class="exit-button" title="Đóng (ESC)" @click="exitForm"></div>
        </div>
        <div class="title-dialog">
          <div class="title">Thông tin nhà cung cấp</div>
          <div class="selection-type">
            <RadioButton
              :button="vendorTypeRadio.button"
              :id="vendorTypeRadio.id"
              :name="vendorTypeRadio.name"
              v-model="vendor.VendorType"
            />
          </div>
          <div class="selection-type">
            <MCheckBox :rowId="'customer'" v-model="isCustomer" />
            <div class="text">Là khách hàng</div>
          </div>
        </div>
        <!-- Tổ chức -->
        <div
          class="content-dialog"
          v-if="vendor.VendorType == 0 || vendor.VendorType == undefined"
        >
          <div class="form">
            <div class="row">
              <div class="left m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="" title="Mã nhân viên">Mã số thuế</label>
                    <div class="field-input">
                      <MInput
                        ref="TaxCode"
                        v-model="vendor.TaxCode"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 12px">
                    <label for="" title="Họ và tên nhân viên"
                      >Mã nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput
                        ref="VendorCode"
                        v-model="vendor.VendorCode"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for=""
                      >Tên nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput
                        ref="VendorName"
                        v-model="vendor.VendorName"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Địa chỉ</label>
                    <div class="field-input">
                      <MTextArea
                        :placeholder="'VD: Số 82 Duy Tân, Dịch Vọng, Cầu Giấy, Hà Nội'"
                        v-model="vendor.Address"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="right m-col-6">
                <div class="row">
                  <div class="m-col-5">
                    <label for="">Điện thoại</label>
                    <div class="field-input">
                      <MInput
                        ref="PhoneNumber"
                        v-model="vendor.PhoneNumber"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 16px">
                    <label for="">Website</label>
                    <div class="field-input">
                      <MInput
                        ref="Website"
                        v-model="vendor.Website"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Nhóm nhà cung cấp</label>
                    <div class="field-input">
                      <MComboBoxMulti
                        :displayProps="vendorGroupProp"
                        :Api="'http://localhost:5093/api/v1/VendorGroups'"
                        :id="'VendorGroupId'"
                        :name="'VendorGroupName'"
                        :code="'VendorGroupCode'"
                        v-model="vendorGroup"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="" title="Nơi cấp chứng minh nhân dân"
                      >Nhân viên mua hàng</label
                    >
                    <MCombobox
                      :displayProps="employeeProp"
                      :apiData="'http://localhost:5093/api/v1/Employees'"
                      :isObject="true"
                      :idProp="'EmployeeId'"
                      :nameProp="'EmployeeCode'"
                      v-model="vendor.EmployeeId"
                      :readonly="readonly"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="tab-form">
                <div class="tab">
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'contact' }"
                    @click="selectTab('contact')"
                  >
                    <div class="text">Liên hệ</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'roles' }"
                    @click="selectTab('roles')"
                  >
                    <div class="text">Điều khoản thanh toán</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'banks' }"
                    @click="selectTab('banks')"
                  >
                    <div class="text">Tài khoản ngân hàng</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'address' }"
                    @click="selectTab('address')"
                  >
                    <div class="text">Địa chỉ khác</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'note' }"
                    @click="selectTab('note')"
                  >
                    <div class="text">Ghi chú</div>
                  </div>
                </div>
                <div class="form-in-tab">
                  <!-- contact tab -->
                  <div class="tab-content" v-show="tabSelected == 'contact'">
                    <div class="left m-col-6">
                      <label for="">Người liên hệ</label>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-4" style="padding-right: 8px">
                            <div class="field-input">
                              <MCombobox
                                :placeholder="'Xưng hô'"
                                :defaultList="
                                  this.dataStorage.vendorDetail
                                    .prefixNameCombobox.button
                                "
                                :idProp="
                                  this.dataStorage.vendorDetail
                                    .prefixNameCombobox.id
                                "
                                :nameProp="
                                  this.dataStorage.vendorDetail
                                    .prefixNameCombobox.name
                                "
                                v-model="vendor.PrefixNameId"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                          <div class="m-col-8">
                            <div class="field-input">
                              <MInput
                                v-model="vendor.ContactName"
                                :placeholder="'Họ và tên'"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.ContactEmail"
                              :placeholder="'ContactEmail'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.ContactPhoneNumber"
                              :placeholder="'Số điện thoại'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div
                        class="row"
                        style="display: block; padding-top: 4px"
                        v-if="vendor.IsCustomer"
                      >
                        <label for="" title="Đại diện theo pháp luật"
                          >Đại diện theo PL</label
                        >
                        <div class="row">
                          <div class="m-col-12">
                            <div class="field-input">
                              <MInput
                                :placeholder="'Đại diện theo pháp luật'"
                                v-model="vendor.ContactLegalRep"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <div v-if="!vendor.IsCustomer">
                        <label for="" title="Đại diện theo pháp luật"
                          >Đại diện theo PL</label
                        >
                        <div class="row">
                          <div class="m-col-12">
                            <div class="field-input">
                              <MInput
                                :placeholder="'Đại diện theo pháp luật'"
                                v-model="vendor.ContactLegalRep"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div v-if="vendor.IsCustomer">
                        <label for="" title="Đại diện theo pháp luật"
                          >Người nhận hóa đơn điện tử</label
                        >
                        <div class="row">
                          <div class="m-col-12">
                            <div class="field-input">
                              <MInput
                                :placeholder="'Họ và tên'"
                                v-model="vendor.ContactName"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                        <div class="row">
                          <div class="m-col-12">
                            <div class="field-input">
                              <MInput
                                :placeholder="'Email'"
                                v-model="vendor.ContactEmail"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                        <div class="row">
                          <div class="m-col-6">
                            <div class="field-input">
                              <MInput
                                :placeholder="'Số điện thoại'"
                                v-model="vendor.ContactPhoneNumber"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- roles tab -->
                  <div
                    class="tab-content"
                    style="display: block"
                    v-show="tabSelected == 'roles'"
                  >
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Điều khoản thanh toán"
                          >Điều khoản thanh toán</label
                        >
                        <div class="field-input">
                          <MCombobox
                            ref="ContractId"
                            :displayProps="ContractProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.contractCombobox
                            "
                            :isObject="true"
                            :idProp="'ContractId'"
                            :nameProp="'ContractCode'"
                            v-model="vendor.ContractId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 12px">
                        <label for="" title="Ngày nợ tối đa"
                          >Số ngày được nợ</label
                        >
                        <div class="field-input">
                          <MInput
                            ref="MaxDebitDate"
                            v-model="vendor.MaxDebitDate"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 12px">
                        <label for="" title="Số nợ tối đa">Số nợ tối đa</label>
                        <div class="field-input">
                          <MInput
                            ref="MaxDebitAmount"
                            v-model="vendor.MaxDebitAmount"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                    </div>
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Tài khoản công nợ phải trả"
                          >Tài khoản công nợ phải trả</label
                        >
                        <div class="field-input">
                          <MCombobox
                            :displayProps="debitAccountProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.debitAccount
                            "
                            :isObject="true"
                            :idProp="'DebitPaymentAccountId'"
                            :nameProp="'DebitPaymentAccountCode'"
                            v-model="vendor.DebitPaymentAccountId"
                            ref="DebitPaymentAccountId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div
                        class="m-col-3"
                        v-if="vendor.IsCustomer"
                        style="padding-left: 12px"
                      >
                        <label for="" title="Số điện thoại di động"
                          >Tài khoản công nợ phải thu</label
                        >
                        <div class="field-input">
                          <MCombobox
                            :displayProps="debitAccountProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.debitAccount
                            "
                            :isObject="true"
                            :idProp="'DebitPaymentAccountId'"
                            :nameProp="'DebitPaymentAccountCode'"
                            v-model="vendor.DebitReceiptAccountId"
                            ref="DebitReceiptAccountId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- bank account -->
                  <div
                    class="tab-content"
                    style="display: block; height: 220px"
                    v-show="tabSelected == 'banks'"
                  >
                    <div
                      class="row m-col-12 edit-table"
                      style="height: calc(100% - 34px); overflow: auto"
                    >
                      <MTableEditable
                        :deleteFunc="true"
                        :columns="columnOfBankTab"
                        :checkBox="false"
                        :defaultData="bankInfo"
                        @getData="getDataOfBank"
                        :readonly="readonly"
                      />
                    </div>
                    <div
                      class="row"
                      style="
                        padding-bottom: 0px;
                        position: sticky;
                        bottom: 1px;
                        background: #fff;
                        width: 100%;
                        z-index: 20;
                      "
                    >
                      <div class="button-area">
                        <div class="add-row" @click="addRow">
                          <button>Thêm dòng</button>
                        </div>
                        <div class="delete-all" @click="deleteAllRow">
                          <button>Xóa hết dòng</button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'address'">
                    <div class="left m-col-6">
                      <label for="">Vị trí địa lý</label>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-6" style="padding-right: 8px">
                            <div class="field-input">
                              <MCombobox
                                ref="CountryId"
                                :placeholder="'Quốc gia'"
                                :idProp="'CountryId'"
                                :nameProp="'CountryName'"
                                v-model="vendor.CountryId"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                          <div class="m-col-6">
                            <div class="field-input">
                              <MCombobox
                                ref="ProvinceId"
                                :idProp="'ProvinceId'"
                                :nameProp="'ProvinceName'"
                                v-model="vendor.ProvinceId"
                                :placeholder="'Tỉnh'"
                                :readonly="readonly"
                                @getData="getData('Province')"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-6" style="padding-right: 8px">
                            <div class="field-input">
                              <MCombobox
                                ref="DistrictId"
                                :placeholder="'Huyện'"
                                :idProp="'DistrictId'"
                                :nameProp="'DistrictName'"
                                v-model="vendor.DistrictId"
                                :readonly="readonly"
                                @getData="getData('District')"
                              />
                            </div>
                          </div>
                          <div class="m-col-6">
                            <div class="field-input">
                              <MCombobox
                                ref="WardId"
                                :placeholder="'Xã'"
                                :idProp="'WardId'"
                                :nameProp="'WardName'"
                                v-model="vendor.WardId"
                                :readonly="readonly"
                                @getData="getData('Ward')"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <label for="" title="Địa chỉ giao hàng"
                        >Địa chỉ giao hàng</label
                      >
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              :placeholder="'Địa chỉ giao hàng'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'note'">
                    <div class="m-col-12">
                      <MTextArea
                        :rows="11"
                        v-model="vendor.TextNote"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="bottom-line"></div>
          <div class="footer">
            <div class="footer-button">
              <div class="left">
                <div class="cancel-button">
                  <MButton @click="exitForm" :text="'Hủy'" ref="cancelButton" />
                </div>
              </div>
              <div class="right">
                <div class="save-button" style="margin: 0px 9.75px">
                  <MButton :text="'Cất'" @click="saveVendor" />
                </div>
                <div class="add-save-button">
                  <MButton
                    :buttonType="'primary'"
                    :text="'Cất và Thêm'"
                    @click="saveVendor"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- Cá nhân -->
        <div class="content-dialog" v-if="vendor.VendorType == 1">
          <div class="form">
            <div class="row" style="align-items: start">
              <div class="left m-col-6">
                <div class="row">
                  <div class="m-col-7">
                    <label for="" title="Họ và tên nhân viên"
                      >Mã nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="field-input">
                      <MInput
                        ref="VendorCode"
                        v-model="vendor.VendorCode"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                  <div class="m-col-5" style="padding-left: 12px">
                    <label for="" title="Mã nhân viên">Mã số thuế</label>
                    <div class="field-input">
                      <MInput
                        ref="TaxCode"
                        v-model="vendor.TaxCode"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for=""
                      >Tên nhà cung cấp <span class="required">*</span></label
                    >
                    <div class="m-d-flex">
                      <div class="m-col-4">
                        <div class="field-input">
                          <MCombobox
                            :defaultList="
                              this.dataStorage.vendorDetail.prefixNameCombobox
                                .button
                            "
                            :idProp="
                              this.dataStorage.vendorDetail.prefixNameCombobox
                                .id
                            "
                            :nameProp="
                              this.dataStorage.vendorDetail.prefixNameCombobox
                                .name
                            "
                            :placeholder="'Xưng hô'"
                            v-model="vendor.PrefixNameId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div class="m-col-8" style="padding-left: 12px">
                        <div class="field-input">
                          <MInput
                            ref="VendorName"
                            v-model="vendor.VendorName"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Địa chỉ</label>
                    <div class="field-input">
                      <MTextArea
                        :placeholder="'VD: Số 82 Duy Tân, Dịch Vọng, Cầu Giấy, Hà Nội'"
                        v-model="vendor.Address"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="right m-col-6" style="align-item: start">
                <!-- <div class="row">
                  <div class="m-col-5">
                    <label for="">Điện thoại</label>
                    <div class="field-input">
                      <MInput ref="PhoneNumber" v-model="vendor.PhoneNumber" />
                    </div>
                  </div>
                  <div class="m-col-7" style="padding-left: 16px">
                    <label for="">Website</label>
                    <div class="field-input">
                      <MInput ref="Website" v-model="vendor.Website" />
                    </div>
                  </div>
                </div> -->
                <div class="row">
                  <div class="m-col-12">
                    <label for="">Nhóm nhà cung cấp</label>
                    <div class="field-input">
                      <MComboBoxMulti
                        :displayProps="vendorGroupProp"
                        :Api="'http://localhost:5093/api/v1/VendorGroups'"
                        :id="'VendorGroupId'"
                        :name="'VendorGroupName'"
                        :code="'VendorGroupCode'"
                        v-model="vendor.VendorGroup"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="m-col-12">
                    <label for="" title="Nơi cấp chứng minh nhân dân"
                      >Nhân viên mua hàng</label
                    >
                    <div class="field-input">
                      <MCombobox
                        :displayProps="employeeProp"
                        :apiData="'http://localhost:5093/api/v1/Employees'"
                        :isObject="true"
                        :idProp="'EmployeeId'"
                        :nameProp="'EmployeeName'"
                        v-model="vendor.EmployeeId"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="tab-form">
                <div class="tab">
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'contact' }"
                    @click="selectTab('contact')"
                  >
                    <div class="text">Liên hệ</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'roles' }"
                    @click="selectTab('roles')"
                  >
                    <div class="text">Điều khoản thanh toán</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'banks' }"
                    @click="selectTab('banks')"
                  >
                    <div class="text">Tài khoản ngân hàng</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'address' }"
                    @click="selectTab('address')"
                  >
                    <div class="text">Địa chỉ khác</div>
                  </div>
                  <div
                    class="tab-item"
                    :class="{ selected: tabSelected == 'note' }"
                    @click="selectTab('note')"
                  >
                    <div class="text">Ghi chú</div>
                  </div>
                </div>
                <div class="form-in-tab">
                  <!-- contact tab -->
                  <div class="tab-content" v-show="tabSelected == 'contact'">
                    <div class="left m-col-6">
                      <label for="">Thông tin liên hệ</label>
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.ContactEmail"
                              :placeholder="'Email'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.ContactPhoneNumber"
                              :placeholder="'Số điện thoại'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.LandLineNumber"
                              :placeholder="'Số điện thoại'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row" style="display: block; padding-top: 4px">
                        <label for="" title="Đại diện theo pháp luật"
                          >Đại diện theo PL</label
                        >
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              :placeholder="'Đại diện theo pháp luật'"
                              v-model="vendor.ContactLegalRep"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <label for="">Thông tin Thẻ căn cước</label>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.IdentifyNumber"
                              :placeholder="'Số chứng minh nhân dân'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-6">
                          <div class="field-input">
                            <!-- eslint-disable -->
                            <DatePicker
                              v-model:value="IdentifyDate"
                              :format="'DD/MM/YYYY'"
                              :placeholder="'Ngày cấp'"
                              :lang="'vi'"
                              :clearable="false"
                              :disabled-date="afterToday"
                              title-format="DD/MM/YYYY"
                            />
                            <!-- eslint-enable -->
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              v-model="vendor.IdentifyPlace"
                              :placeholder="'Nơi cấp'"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- roles tab -->
                  <div
                    class="tab-content"
                    style="display: block"
                    v-show="tabSelected == 'roles'"
                  >
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Số điện thoại di động"
                          >Điều khoản thanh toán</label
                        >
                        <div class="field-input">
                          <MCombobox
                            ref="ContractId"
                            :displayProps="ContractProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.contractCombobox
                            "
                            :isObject="true"
                            :idProp="'ContractId'"
                            :nameProp="'ContractCode'"
                            v-model="vendor.EmployeeId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 12px">
                        <label for="" title="Số điện thoại cố định"
                          >Số ngày được nợ</label
                        >
                        <div class="field-input">
                          <MInput
                            ref="LandLineNumber"
                            :readonly="readonly"
                            v-model="vendor.LandLineNumber"
                          />
                        </div>
                      </div>
                      <div class="m-col-3" style="padding-left: 12px">
                        <label for="" title="Địa chỉ email">Số nợ tối đa</label>
                        <div class="field-input">
                          <MInput
                            ref="Email"
                            v-model="vendor.Email"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                    </div>
                    <div class="row m-col-12">
                      <div class="m-col-3">
                        <label for="" title="Số điện thoại di động"
                          >Tài khoản công nợ phải trả</label
                        >
                        <div class="field-input">
                          <MCombobox
                            :displayProps="debitAccountProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.debitAccount
                            "
                            :isObject="true"
                            :idProp="'DebitPaymentAccountId'"
                            :nameProp="'DebitPaymentAccountCode'"
                            v-model="vendor.DebitPaymentAccountId"
                            ref="DebitPaymentAccountId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                      <div
                        class="m-col-3"
                        v-if="vendor.IsCustomer"
                        style="padding-left: 12px"
                      >
                        <label for="" title="Số điện thoại di động"
                          >Tài khoản công nợ phải thu</label
                        >
                        <div class="field-input">
                          <MCombobox
                            :displayProps="debitAccountProp"
                            :defaultList="
                              this.dataStorage.vendorDetail.debitAccount
                            "
                            :isObject="true"
                            :idProp="'DebitPaymentAccountId'"
                            :nameProp="'DebitPaymentAccountCode'"
                            v-model="vendor.DebitReceiptAccountId"
                            ref="DebitReceiptAccountId"
                            :readonly="readonly"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- bank account -->
                  <div
                    class="tab-content"
                    style="display: block; height: 220px"
                    v-show="tabSelected == 'banks'"
                  >
                    <div
                      class="row m-col-12 edit-table"
                      style="height: calc(100% - 34px); overflow: auto"
                    >
                      <MTableEditable
                        :deleteFunc="true"
                        :columns="columnOfBankTab"
                        :checkBox="false"
                        :defaultData="bankInfo"
                        @getData="getDataOfBank"
                        :readonly="readonly"
                      />
                    </div>
                    <div
                      class="row"
                      style="
                        padding-bottom: 0px;
                        position: sticky;
                        bottom: 1px;
                        background: #fff;
                        width: 100%;
                        z-index: 20;
                      "
                    >
                      <div class="button-area">
                        <div class="add-row" @click="addRow">
                          <button>Thêm dòng</button>
                        </div>
                        <div class="delete-all" @click="deleteAllRow">
                          <button>Xóa hết dòng</button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'address'">
                    <div class="left m-col-6">
                      <label for="">Vị trí địa lý</label>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-6" style="padding-right: 8px">
                            <div class="field-input">
                              <MCombobox
                                ref="CountryId"
                                :placeholder="'Quốc gia'"
                                :idProp="'CountryId'"
                                :nameProp="'CountryName'"
                                v-model="vendor.CountryId"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                          <div class="m-col-6">
                            <div class="field-input">
                              <MCombobox
                                ref="ProvinceId"
                                :idProp="'ProvinceId'"
                                :nameProp="'ProvinceName'"
                                v-model="vendor.ProvinceId"
                                :placeholder="'Tỉnh'"
                                :readonly="readonly"
                                @getData="getData(isLoad, 'ProvinceId')"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="m-col-12 m-d-flex">
                          <div class="m-col-6" style="padding-right: 8px">
                            <div class="field-input">
                              <MCombobox
                                ref="DistrictId"
                                :placeholder="'Huyện'"
                                :idProp="'DistrictId'"
                                :nameProp="'DistrictName'"
                                v-model="vendor.DistrictId"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                          <div class="m-col-6">
                            <div class="field-input">
                              <MCombobox
                                ref="WardId"
                                :placeholder="'Xã'"
                                :idProp="'WardId'"
                                :nameProp="'WardName'"
                                v-model="vendor.WardId"
                                :readonly="readonly"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="right m-col-6">
                      <label for="" title="Địa chỉ giao hàng"
                        >Địa chỉ giao hàng</label
                      >
                      <div class="row">
                        <div class="m-col-12">
                          <div class="field-input">
                            <MInput
                              :placeholder="'Đại diện theo pháp luật'"
                              v-model="vendor.ContactLegalRep"
                              :readonly="readonly"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- addres tab -->
                  <div class="tab-content" v-show="tabSelected == 'note'">
                    <div class="m-col-12">
                      <MTextArea
                        :rows="11"
                        v-model="vendor.TextNote"
                        :readonly="readonly"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="bottom-line"></div>
          <div class="footer">
            <div class="footer-button">
              <div class="left">
                <div class="cancel-button">
                  <MButton @click="exitForm" :text="'Hủy'" ref="cancelButton" />
                </div>
              </div>
              <div class="right">
                <div class="save-button" style="margin: 0px 9.75px">
                  <MButton :text="'Cất'" @click="saveVendor()" />
                </div>
                <div class="add-save-button">
                  <MButton
                    :buttonType="'primary'"
                    :text="'Cất và Thêm'"
                    @click="saveVendor()"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- <ConfirmDialog
      :text="confirmDialogData.text"
      :type="confirmDialogData.type"
      :listButton="confirmDialogData.button"
      :align="confirmDialogData.align"
      @confirm="confirm"
      :keyConfirm="confirmDialogData.key"
      v-if="isConfirm"
    /> -->
  </div>
</template>

<script>
//component
import MCheckBox from "@/components/base/BaseCheckBox.vue";
import MInput from "@/components/base/input/BaseInput.vue";
import RadioButton from "@/components/base/BaseRadioButton.vue";
import MTextArea from "@/components/base/input/BaseTextArea.vue";
import MCombobox from "@/components/base/BaseComboBox.vue";
import MComboBoxMulti from "@/components/base/BaseComboBoxMulti.vue";
import MButton from "@/components/base/button/BaseButton.vue";
import MTableEditable from "@/components/base/table/BaseTableEditable.vue";
import DatePicker from "vue-datepicker-next";
import "vue-datepicker-next/index.css";
// import ConfirmDialog from "@/components/base/dialog/BaseConfirmDialog.vue";
import axios from "axios";

export default {
  name: "VendorDetail",
  components: {
    MCheckBox,
    MInput,
    RadioButton,
    MTextArea,
    MCombobox,
    MButton,
    DatePicker,
    MTableEditable,
    MComboBoxMulti,
  },
  data() {
    return {
      vendorGroupProp: [
        {
          id: "VendorGroupName",
          name: "Tên nhà cung cấp",
        },
        {
          id: "VendorGroupCode",
          name: "Mã nhà cung cấp",
        },
        {
          id: "CreateBy",
          name: "Người tạo",
        },
      ],
      //các thuôc jtinsh hiển thị trong combobox employee
      employeeProp: [
        {
          id: "EmployeeCode",
          name: "Mã nhân viên",
        },
        {
          id: "EmployeeName",
          name: "Tên nhân viên",
        },
      ],
      ContractProp: [
        {
          id: "ContractCode",
          name: "Mã điều khoản",
        },
        {
          id: "ContractName",
          name: "Tên điều khoản",
        },
      ],
      debitAccountProp: [
        {
          id: "DebitPaymentAccountCode",
          name: "Tài khoản",
        },
        {
          id: "DebitPaymentAccountName",
          name: "Tên tài khoản",
        },
      ],
      listTabIndex: [],
      //thông tin các cột cho tab nhập tài khoản ngân hàng
      columnOfBankTab: [
        {
          id: "BankAccount",
          name: "Số tài khoản",
          displayName: "Số tài khoản",
          descriptionName: "Số tài khoản",
          width: "150px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "BankName",
          name: "Tên ngân hàng",
          displayName: "Tên ngân hàng",
          descriptionName: "Tên ngân hàng",
          width: "auto",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "BankBranch",
          name: "Chi nhánh",
          displayName: "Chi nhánh",
          descriptionName: "Chi nhánh",
          width: "100px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
        {
          id: "District",
          name: "Tỉnh/TP của ngân hàng",
          displayName: "Tỉnh/TP của ngân hàng",
          descriptionName: "Tỉnh/TP của ngân hàng",
          width: "100px",
          minWidth: "120px",
          classList: [],
          align: "left",
          edit: true,
          count: false,
          type: "text",
        },
      ],
      //data cho radiobutton loại nhà cũng cấp
      vendorTypeRadio: this.dataStorage.vendorDetail.radioButtonTypeVendor,
      //đối tượng nhà cung cấp
      vendor: {},
      //xác định tab được chọn
      tabSelected: "contact",
      //xác đinh là form thêm hay sửa
      isAdd: true,
      //vendorId được gán khi mở form sửa
      vendorId: "",
      //xác định form đang được xem không(xem thì k sửa và thêm)
      readonly: false,
    };
  },
  computed: {
    bankInfo: {
      get() {
        try {
          let rs = JSON.parse(this.vendor.Bank ? this.vendor.Bank : "[{}]");
          return rs;
        } catch (error) {
          console.log(error);
          return [
            { BankAccount: "", BankName: "", BankBranch: "", Distict: "" },
          ];
        }
      },
      set(val) {
        try {
          this.vendor.Bank = val;
        } catch (error) {
          console.log(error);
          this.vendor.Bank = JSON.stringify([
            { BankAccount: "", BankName: "", BankBranch: "", Distict: "" },
          ]);
        }
      },
    },
    isCustomer: {
      get() {
        try {
          return this.vendor.IsCustomer == 1;
        } catch (error) {
          console.log(error);
          return false;
        }
      },
      set(val) {
        try {
          val ? (this.vendor.IsCustomer = 1) : (this.vendor.IsCustomer = 0);
        } catch (error) {
          console.log(error);
        }
      },
    },
    vendorGroup: {
      get() {
        try {
          let rs = this.vendor.VendorGroup ? this.vendor.VendorGroup : "";
          return rs;
        } catch (error) {
          console.log(error);
          return "";
        }
      },
      set(val) {
        try {
          this.vendor.VendorGroup = val;
        } catch (error) {
          console.log(error);
          this.vendor.VendorGroup = JSON.stringify([]);
        }
      },
    },
    IdentifyDate: {
      get() {
        try {
          let rs = this.vendor.IdentifyDate;
          return new Date(rs);
        } catch (error) {
          console.log(error);
          return "";
        }
      },
      set(val) {
        try {
          this.vendor.IdentifyDate = new Date(val);
        } catch (error) {
          console.log(error);
        }
      },
    },
  },
  methods: {
    /**
     * Mô tả : chọn chuyển tab form
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    selectTab(tabName) {
      try {
        this.tabSelected = tabName;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm 1 dòng dữ liệu mới cho table
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    addRow() {
      try {
        let bank = JSON.parse(this.vendor.Bank);
        bank.push({
          BankAccount: "",
          BankName: "",
          BankBranch: "",
          Distict: "",
        });
        this.vendor.Bank = JSON.stringify(bank);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy dữ liệu tử table ngân hàng
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    getDataOfBank(data) {
      try {
        this.vendor.Bank = JSON.stringify(data);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm 1 dòng dữ liệu mới cho table
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    deleteAllRow() {
      try {
        this.vendor.Bank = JSON.stringify([
          { BankAccount: "", BankName: "", BankBranch: "", Distict: "" },
        ]);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lưu danh sách nhà cung cấp và đóng form
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    saveVendor() {
      console.log(this.vendor);
      try {
        if (this.isAdd) {
          this.addVendor();
        } else {
          this.editVendor();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm vendor
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    addVendor() {
      try {
        let me = this;
        axios({
          url: "http://localhost:5093/api/v1/Vendors",
          method: "POST",
          contentType: "application/json",
          data: me.vendor,
        })
          .then((res) => {
            console.log(res);
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : thêm vendor
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    editVendor() {
      try {
        let me = this;
        axios({
          url: `http://localhost:5093/api/v1/Vendors/${me.vendor.VendorId}`,
          method: "PUT",
          contentType: "application/json",
          data: me.vendor,
        })
          .then((res) => {
            console.log(res);
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy đối tượng vendor cho form sửa
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    getVendor(id) {
      try {
        if (!id) return;
        let me = this;
        axios
          .get(`http://localhost:5093/api/v1/Vendors/${id}`)
          .then((res) => {
            console.log(res);
            me.vendor = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy mã nhà cung cáp mới nhất cho form thêm
     * Created by: Nguyễn Đức Toán - MF1095 (21/05/2022)
     */
    getNewVendorCode() {
      try {
        let me = this;
        axios
          .get(`http://localhost:5093/api/v1/Vendors/NewVendorCode`)
          .then((res) => {
            console.log(res);
            me.vendor.VendorCode = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : đóng form chi tiết
     * Created by: Nguyễn Đức Toán - MF1095 (11/05/2022)
     */
    exitForm() {
      try {
        this.readonly = false;
        this.$emit("exitForm", true);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy ds tỉnh theo quốc gia
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    getProvince() {
      try {
        if (!this.vendor.CountryId) return;
        let me = this;
        axios
          .get(
            `http://wikunde.tk:5000/api/v1/Provincies/ByCountry/${me.vendor.CountryId}`
          )
          .then((res) => {
            me.$refs["ProvinceId"].setData(
              JSON.parse(JSON.stringify(res.data))
            );
            if (me.readonly) {
              me.getDistrict();
            }
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy ds huyện theo tỉnh
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    getDistrict() {
      try {
        if (!this.vendor.ProvinceId) return;
        let me = this;
        axios
          .get(
            `http://wikunde.tk:5000/api/v1/Districts/ByProvince/${me.vendor.ProvinceId}`
          )
          .then((res) => {
            me.$refs["DistrictId"].setData(
              JSON.parse(JSON.stringify(res.data))
            );
            if (me.readonly) {
              me.getWard();
            }
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy ds xã theo huyện
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    getWard() {
      try {
        if (!this.vendor.DistrictId) return;
        let me = this;
        axios
          .get(
            `http://wikunde.tk:5000/api/v1/Wards/ByDistrict/${me.vendor.DistrictId}`
          )
          .then((res) => {
            me.$refs["WardId"].setData(JSON.parse(JSON.stringify(res.data)));
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : lấy ds quốc gia
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    getCountry() {
      try {
        let me = this;
        axios
          .get(`http://wikunde.tk:5000/api/v1/Countries`)
          .then((res) => {
            me.$refs["CountryId"].setData(JSON.parse(JSON.stringify(res.data)));
            if (me.readonly) {
              me.getProvince();
            }
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /*,*
     * Mô tả : lấy dữ liệu địa chỉ
     * Created by: Nguyễn Đức Toán - MF1095 (22/05/2022)
     */
    getData(key) {
      try {
        let me = this;
        switch (key) {
          case "Country":
            me.getCountry();
            break;
          case "Province":
            me.getProvince();
            break;
          case "District":
            me.getDistrict();
            break;
          case "Ward":
            me.getWard();
            break;

          default:
            break;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    vendor: {
      deep: true,
      handler(newVal) {
        console.log(newVal);
      },
    },
  },
  mounted() {
    try {
      this.vendor.Bank = JSON.stringify([
        { BankAccount: "", BankName: "", BankBranch: "", Distict: "" },
      ]);
      this.vendor.VendorType = "0";
      this.$nextTick(() => {
        this.getCountry();
      });
    } catch (error) {
      console.log(error);
    }
  },
};
</script>

<style >
@import url("@/css/components/dialog.css");
.content-dialog .form {
  overflow: auto !important;
}
.content-dialog .form .tab-form {
  width: calc(100% - 1px);
}
.content-dialog .form .tab-form .tab {
  display: flex;
  align-items: flex-end;
  height: 32px;
}
.content-dialog .form .tab-form .tab .tab-item {
  display: flex;
  align-items: center;
  border: 1px solid #babec5;
  border-bottom: none;
  margin-right: 4px;
  padding: 3px 8px;
  cursor: pointer;
  border-top-left-radius: 2px;
  border-top-right-radius: 2px;
}
.content-dialog .form .tab-form .tab .tab-item.selected {
  padding: 5px 8px;
  background-color: #cce4f9;
}
.content-dialog .form .tab-form .tab .tab-item:hover .text {
  color: #2ca01c;
}
.content-dialog .form .tab-form .tab .tab-item .text {
  text-align: center;
}
.content-dialog .form .tab-form .form-in-tab {
  min-height: 220px;
  border: 1px solid #babec5;
  width: 100%;
}
.content-dialog .form .tab-form .form-in-tab .tab-content {
  width: 100%;
  display: flex;
  padding: 10px;
}
.content-dialog .form {
  overflow: auto !important;
}
.form-in-tab .tab-content .row {
  padding-bottom: 0 !important;
}
.tab-content .button-area {
  display: flex;
  align-items: center;
}
.tab-content .button-area .add-row {
  height: 24px;
  margin-right: 12px;
}
.tab-content .button-area button {
  background-color: transparent;
  border: 1px solid #ccc;
  height: 100%;
  border-radius: 2px;
  color: #111;
  font-size: 12px;
  font-weight: 600;
  padding: 2px 16px;
  cursor: pointer;
}
.tab-content .button-area button:hover {
  background-color: #eceef1;
}
.tab-content .button-area button:active {
  background-color: #bbb;
}
.tab-content .button-area .delete-all {
  height: 24px;
}
.m-d-flex {
  display: flex;
}

.edit-table .hidden-left {
  display: none !important;
}
.edit-table .hidden-right {
  display: none !important;
}
</style>