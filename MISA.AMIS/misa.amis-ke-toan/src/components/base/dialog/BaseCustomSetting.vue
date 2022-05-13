<template>
  <div class="custom-setting">
    <div class="dialog-content">
      <div>
      <div class="title-area">
        <div class="title">Tùy chỉnh giao diện</div>
        <div class="func-btn">
          <div class="btn help-btn" style="margin-right: 4px">
            <div class="ic-help-dialog"></div>
          </div>
          <div class="btn exit-btn" title="Exit (Esc)" @click="exitSetting">
            <div class="ic-exit-dialog"></div>
          </div>
        </div>
      </div>
      <div
        class="tools-area m-d-flex"
        style="justify-content: space-between; align-items: center"
      >
        <div class="input m-col-4">
          <MInput
            :placeholder="'Nhập từ khóa tìm kiếm'"
            :classIcon="'search'"
          />
        </div>
        <div class="link">
          <a>Sửa tên cột và độ rộng</a>
        </div>
      </div>
      <div class="container">
        <div class="table-area">
          <MTableEditable />
        </div>
        <div class="view-other-area"></div>
      </div></div>
      <div class="footer-area">
        <div class="left">
          <div class="cancel-button">
            <MButton @click="exitSetting" :text="'Hủy'" ref="cancelButton" />
          </div>
        </div>
        <div class="right">
          <div class="save-button" style="margin: 0px 9.75px">
            <MButton :text="'Lấy mẫu ngầm định'" />
          </div>
          <div class="add-save-button">
            <MButton
              :buttonType="'primary'"
              :text="'Cất'"
              @keydown="lastTabIndex"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MInput from "@/components/base/input/BaseInput.vue";
import MTableEditable from "@/components/base/table/BaseTableEditable.vue";
import MButton from "@/components/base/button/BaseButton.vue";
export default {
  name: "MSetting",
  components: {
    MInput,
    MTableEditable,
    MButton,
  },
  data() {
    return {};
  },
  methods: {
    exitSetting() {
      try {
        this.$emit("exitSetting", true);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
.custom-setting {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: #11111121;
}
.custom-setting .dialog-content {
  width: 50%;
  float: right;
  height: 100%;
  background-color: #fff;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
.custom-setting .dialog-content .container {
  overflow: auto;
}
.custom-setting .dialog-content .title-area {
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: sticky;
  left: 0;
}
.custom-setting .dialog-content .title-area .title {
  padding: 16px 20px 8px 20px;
  font-size: 24px;
  font-weight: 700;
}
.custom-setting .dialog-content .title-area .func-btn {
  display: flex;
  align-items: center;
  padding: 12px;
}
.custom-setting .dialog-content .title-area .func-btn .btn:hover {
  cursor: pointer;
}
.custom-setting .dialog-content .tools-area {
  padding: 8px 20px;
  position: sticky;
  left: 0;
}
.custom-setting .dialog-content .table-area {
  padding: 8px 0px;
  display: flex;
  justify-content: center;
}
.custom-setting .dialog-content .footer-area {
  padding: 8px 20px;
  position: sticky;
  left: 0;
  display: flex;
  justify-content: space-between;
}
.custom-setting .dialog-content .footer-area .right {
  display: flex;
}
.custom-setting .dialog-content .view-orther-area {
  padding: 8px 20px;
  position: sticky;
  left: 0;
}
.hidden-right {
  background-color: #fff !important;
}
</style>