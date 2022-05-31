import { createApp } from 'vue'
import App from './App.vue'

import dataStorage from '@/js/storage/dataStorage.js';
import resource from '@/js/resource/resource.js';
import processResource from '@/js/resource/caProcessResource.js';
import confirmResource from '@/js/resource/confirmResource.js';
import { createRouter, createWebHistory } from 'vue-router'



//1.định nghĩa/import các router
// import Employee from '@/view/employee/TheEmployee.vue'
import CAProcess from '@/view/ca/TheCAProcess.vue'
import CAReceipt from '@/view/ca/TheCAReceipt.vue'
import CAReceiptPaymentList from "@/view/ca/CAReceiptPaymentList.vue";



//2. định nghĩa
const routers = [
    { path: '/', redirect: "/CA/CAProcess" },
    {
        path: '/CA',
        component: () =>
            import ('@/view/ca/TheCash.vue'),
        children: [
            { path: '', redirect: "/CA/CAProcess" },
            { path: 'CAProcess', component: CAProcess },
            { path: 'CAReceipt', component: CAReceipt },
            { path: 'CAReceiptPaymentList', component: CAReceiptPaymentList },
        ]

    },


    {
        path: '/ThePaymentDetail',
        name: 'ThePaymentDetail',
        component: () =>
            import ("@/view/ca/ThePaymentDetail.vue")
    },
    {
        path: '/VendorList',
        name: 'VendorList',
        component: () =>
            import ("@/view/vendor/VendorList.vue")
    },
    {
        path: '/test',
        name: 'HelloWorld',
        component: () =>
            import ("@/components/HelloWorld.vue")
    }

];
//2. khởi tạo các router
const router = createRouter({
    history: createWebHistory(process.env.history),
    routes: routers
});
//4. sử dụng vuerouter

let app = createApp(App);

app.config.globalProperties.dataStorage = dataStorage;
app.config.globalProperties.resource = resource;
app.config.globalProperties.processResource = processResource;
app.config.globalProperties.confirmResource = confirmResource;

app.use(router);
app.mount('#app');