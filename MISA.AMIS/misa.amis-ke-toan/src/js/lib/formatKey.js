const formatKey = {
    /**
     * Mô tả : format key qua dạng camelkey
     * Created by: Nguyễn Đức Toán - MF1095 (10/05/2022)
     */
    toCamelKey(key) {
        try {
            return key.toLowerCase().replace(/([-_][a-z])/g, group =>
                group
                .toUpperCase()
                .replace('-', '')
                .replace('_', '')
            );
        } catch (error) {
            console.log(error);
            return key;
        }
    }
}