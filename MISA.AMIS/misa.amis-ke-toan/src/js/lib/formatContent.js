const format = {
    /**
     * hàm để định dạng dữ liệu kiểu ngày tháng cho người dùng dd/mm/yyy
     * @param {*} date - string
     * @returns - string
     */
    dateFormat: (date) => {
        try {
            if (!date) {
                return "";
            } else {
                date = new Date(date);
                let day = date.getDate() >= 10 ? date.getDate() : `0${date.getDate()}`;
                let month =
                    date.getMonth() + 1 >= 10 ?
                    date.getMonth() + 1 :
                    `0${date.getMonth() + 1}`;
                let year = date.getYear() + 1900;
                return `${day}/${month}/${year}`;
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * hàm để định dạng kiểu dữ liệu tiền tệ
     * @param {*} number string hoặc number
     * @returns string
     */
    currencyVNFormat: (number) => {
        try {
            if (!number) return 0;
            let result = Intl.NumberFormat('vi', {
                style: 'currency',
                currency: 'VND',
            }).format(number);
            return result;
        } catch (error) {
            console.log(error);
            return 0;
        }
    },
    /**
     * hàm để định dạng dữ liệu kiểu số
     * @param {*} number string hoặc number
     * @returns string
     */
    numberVNFormat: (number) => {
        try {
            if (!number) return 0;
            let result = Intl.NumberFormat("vi-VN").format(number);
            return result;
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * hàm để định dạng dữ liệu kiểu text vd:tên, email, giới tính, sdt...
     * @param {*} text string hoặc number
     * @returns string
     */
    textFormat: (text) => {
        try {
            // nếu text null|undefined
            if (!text) {
                return "";
            } else {
                return text;
            }
        } catch (error) {
            console.log(error);
        }
    },
    /**
     * hàm để định dạng dữ liệu kiểu ngày tháng dd-mm-yyy
     * @param {*} date - string
     * @returns - string
     */
    toStringDate: (date) => {
        try {
            if (!date) {
                return "";
            } else {
                date = new Date(date);
                let day = date.getDate() >= 10 ? date.getDate() : `0${date.getDate()}`;
                let month =
                    date.getMonth() + 1 >= 10 ?
                    date.getMonth() + 1 :
                    `0${date.getMonth() + 1}`;
                let year = date.getYear() + 1900;
                return `${year}-${month}-${day}`;
            }
        } catch (error) {
            console.log(error);
        }
    },
    /**----------------------------------
     * Hàm set timezone cho ngày tháng
     * @param {Date} date 
     * Author: quyetkaito (18/03/2022)
     ----------------------------------*/
    setTimeZone(date) {
        try {
            date = new Date(date);
            if (date && date != "Invalid Date") {
                var newDate = date;
                newDate.setTime(
                    newDate.getTime() - new Date().getTimezoneOffset() * 60 * 1000
                );
                return newDate.toDateString();
            } else {
                return "";
            }
        } catch (error) {
            console.log(error);
            return ""
        }
    },
}
export default format;