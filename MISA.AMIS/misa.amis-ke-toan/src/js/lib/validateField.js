/* eslint-disable */
const validator = {
    /**
     * Mô tả : validate cho các trường bắt buộc nhập
     * @param
     * @return
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 14:46 19/04/2022
     */
    required(value) {
        return value ? true : false
    },
    /**
     * Mô tả : validate cho các trường email
     * @param {*} value string
     * @return true/false
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 14:44 19/04/2022
     */
    email(value) {
        var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return regex.test(value) ? true : false
    },
    /**
     * Mô tả : validate cho các trường số điện thoại di động ở việt nam
     * @param {*} value string
     * @return true/false
     * Created by: Nguyễn Đức Toán - MF1095
     * Created date: 14:44 19/04/2022
     */
    phone(value) {
        var regex = /(84|0[3|5|7|8|9])+([0-9]{8})\b/;
        return regex.test(value) ? true : false
    },
    //validate cho các trường tên
    name(value) {
        var regex = /^[a-zA-Z ]{2,}$/g
        return regex.test(removeAscent(value)) ? undefined : "chưa đúng định dạng"
    },
    min(min) {
        return function(value) {
            return (value.length > min) ? undefined : `Trường dữ liệu phải chưa đủ ${min} ký tự`
        }
    },
    max(max) {
        return function(value) {
            return (value.length > max) ? undefined : `Trường dữ liệu phải chưa đủ ${max} ký tự`
        }
    },
    removeAscent: (str) => {
        if (str === null || str === undefined) return str;
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        return str;
    }
}

export default validator