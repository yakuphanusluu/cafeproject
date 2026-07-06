const API_URL = "https://cafeproject-2p12.onrender.com/api";
let cart = [];
let trackingInterval;
let currentProduct = null;
let lastStatus = ""; 
let isModalOpen = false;

// ==========================================
// 🌍 DİL PAKETİ VE ÇEVİRİ MOTORU (i18n)
// ==========================================
const translations = {
    tr: {
        // Müşteri Ekranı
        shop_name: "Alanya Cyber Coffee", slogan: "Geleceğin Kahvesi, Bugün Fincanında! 🚀",
        btn_mixology: "🧪 Mixology Lab", login_register: "Giriş Yap / Kaydol", logout: "Çıkış", points_label: "Puanın:", 
        order_slip: "📝 Sipariş Fişi", cup_name: "Bardaktaki İsim?", cash: "Nakit", credit_card: "Kredi Kartı",
        total: "Toplam:", place_order: "Siparişi Ver", status_label: "Durum:",
        free_coffee: "10 Puanla Bedava Al", lbl_size: "Boyut:", 
        size_s: "Küçük", size_m: "Orta (Grande)", size_m_price: "Orta (Grande) (+10 TL)", size_l: "Büyük", size_l_price: "Büyük (+15 TL)", 
        lbl_temp: "Sıcaklık:", temp_hot: "Sıcak", temp_cold: "Soğuk (Buzlu)", temp_ice: "Bol Buzlu", 
        lbl_syrup: "Şurup:", syr_none: "Şurupsuz", syr_caramel: "Karamel", syr_pistachio: "Fıstık", syr_strawberry: "Çilek", syr_vanilla: "Vanilya",
        lbl_extras: "Ekstralar:", ext_none: "Yok", ext_milk: "Ekstra Süt (+10 TL)", ext_shot: "Ekstra Shot (+20 TL)",
        price_label: "Fiyat:", btn_add: "Ekle", btn_cancel: "İptal",
        lbl_mix_shot: "Espresso Shot (20 TL/Shot):", lbl_mix_milk: "Süt Seçimi:", 
        mix_m_none: "Sütsüz", mix_m_normal: "Normal Süt (+10 TL)", mix_m_oat: "Yulaf Sütü (+25 TL)",
        lbl_mix_syrup: "Şuruplar (Adet 15 TL):", potion_price: "İksir Fiyatı:", btn_add_potion: "İksiri Ekle",
        empty_cart: "Sepet boş kanka! ☕", btn_select: "Seç",
        alert_empty: "İsim ve sepet eksik!", alert_success: "Sipariş Baristada! 🚀", btn_sending: "İletiliyor...",
        status_waiting: "Bekliyor", status_preparing: "Hazırlanıyor", status_ready: "Hazır", status_delivered: "Teslim Edildi",
        loading_menu: "Kahveler yükleniyor (Sunucu uyanıyor)... ☕", menu_error: "Menü yüklenemedi.",
        
        // Patron Ekranı
        boss_panel: "📈 Admin Paneli (Finansal Takip)", total_rev: "💰 Toplam Ciro", daily_ord: "📦 Bugünkü Sipariş", active_ord: "⏳ Aktif (Bekleyen)", best_seller: "🔥 Günün Favorisi",
        btn_z: "📊 Gün Sonu Al (Kapat)", daily_logs: "📜 Günlük Sipariş Dökümleri", th_id: "ID", th_cust: "Müşteri", th_details: "Sipariş İçeriği (Detaylar)", th_pay: "Ödeme", th_amt: "Tutar", th_stat: "Durum",
        calculating: "Hesaplanıyor...", none_yet: "Henüz Yok", empty_reg: "Kasa sıfır. Yeni sipariş bekleniyor.",
        z_conf: "GÜN SONU ALINACAK!\n\nTüm açık siparişler kapatılacak ve tablo sıfırlanacak. Emin misiniz?", z_succ: "✅ Gün Sonu Raporu başarıyla alındı!",
        
        // Barista Ekranı
        barista_panel: "🚀 Cyber Coffee Barista Paneli", pending_count: "Bekleyen Sipariş", checking: "Siparişler kontrol ediliyor...",
        no_orders: "Henüz sipariş yok, dükkan sakin.", coffee_det: "☕ Kahveler & Detaylar:", pay_lbl: "💳 Ödeme:",
        btn_prep_act: "⏳ Hazırla", btn_ready_act: "✅ Hazır", btn_deliv_act: "🚀 Teslim", not_spec: "Belirtilmedi", stat_err: "Durum güncellenemedi!",
        
        // Login & Register Ekranı
        login_title: "Giriş Yap", register_title: "Kayıt Ol",
        username_ph: "Kullanıcı Adı", password_ph: "Şifre",
        btn_login: "Giriş Yap", btn_register: "Kayıt Ol",
        toggle_register: "Hesabın yok mu? Kayıt Ol", toggle_login: "Zaten hesabın var mı? Giriş Yap",
        back_home: "⬅ Ana Sayfaya Dön", login_err: "Giriş başarısız! Kullanıcı adı veya şifre hatalı.", reg_err: "Kayıt başarısız! Bu kullanıcı adı alınmış olabilir."
    },
    en: {
        // Customer Screen
        shop_name: "Alanya Cyber Coffee", slogan: "The Future of Coffee, in Your Cup Today! 🚀",
        btn_mixology: "🧪 Mixology Lab", login_register: "Login / Register", logout: "Logout", points_label: "Points:", 
        order_slip: "📝 Order Slip", cup_name: "Name on Cup?", cash: "Cash", credit_card: "Credit Card",
        total: "Total:", place_order: "Place Order", status_label: "Status:",
        free_coffee: "Get Free with 10 Pts", lbl_size: "Size:", 
        size_s: "Small", size_m: "Medium (Grande)", size_m_price: "Medium (Grande) (+10 TL)", size_l: "Large", size_l_price: "Large (+15 TL)", 
        lbl_temp: "Temp:", temp_hot: "Hot", temp_cold: "Cold (Iced)", temp_ice: "Extra Ice", 
        lbl_syrup: "Syrup:", syr_none: "No Syrup", syr_caramel: "Caramel", syr_pistachio: "Pistachio", syr_strawberry: "Strawberry", syr_vanilla: "Vanilla",
        lbl_extras: "Extras:", ext_none: "None", ext_milk: "Extra Milk (+10 TL)", ext_shot: "Extra Shot (+20 TL)",
        price_label: "Price:", btn_add: "Add", btn_cancel: "Cancel",
        lbl_mix_shot: "Espresso Shot (20 TL/Shot):", lbl_mix_milk: "Milk:", 
        mix_m_none: "No Milk", mix_m_normal: "Regular Milk (+10 TL)", mix_m_oat: "Oat Milk (+25 TL)",
        lbl_mix_syrup: "Syrups (15 TL each):", potion_price: "Potion Price:", btn_add_potion: "Add Potion",
        empty_cart: "Cart is empty, mate! ☕", btn_select: "Select",
        alert_empty: "Name or cart is missing!", alert_success: "Order sent to Barista! 🚀", btn_sending: "Sending...",
        status_waiting: "Waiting", status_preparing: "Preparing", status_ready: "Ready", status_delivered: "Delivered",
        loading_menu: "Loading coffees (Waking up server)... ☕", menu_error: "Failed to load menu.",
        
        // Boss Panel
        boss_panel: "📈 Admin Panel (Financial Tracking)", total_rev: "💰 Total Revenue", daily_ord: "📦 Today's Orders", active_ord: "⏳ Active (Pending)", best_seller: "🔥 Best Seller",
        btn_z: "📊 Take Z-Report (Close)", daily_logs: "📜 Daily Order Logs", th_id: "ID", th_cust: "Customer", th_details: "Order Details", th_pay: "Payment", th_amt: "Amount", th_stat: "Status",
        calculating: "Calculating...", none_yet: "None Yet", empty_reg: "Register is empty. Waiting for orders.",
        z_conf: "Z-REPORT WILL BE TAKEN!\n\nAll open orders will be closed and the table will be cleared. Are you sure?", z_succ: "✅ Z-Report taken successfully!",
        
        // Barista Panel
        barista_panel: "🚀 Cyber Coffee Barista Panel", pending_count: "Pending Orders", checking: "Checking orders...",
        no_orders: "No orders yet, shop is quiet.", coffee_det: "☕ Coffees & Details:", pay_lbl: "💳 Payment:",
        btn_prep_act: "⏳ Prepare", btn_ready_act: "✅ Ready", btn_deliv_act: "🚀 Deliver", not_spec: "Not specified", stat_err: "Status update failed!",
        
        // Login & Register Screen
        login_title: "Login", register_title: "Register",
        username_ph: "Username", password_ph: "Password",
        btn_login: "Login", btn_register: "Register",
        toggle_register: "Don't have an account? Register", toggle_login: "Already have an account? Login",
        back_home: "⬅ Back to Home", login_err: "Login failed! Incorrect username or password.", reg_err: "Registration failed! Username might be taken."
    }
};

let currentLang = localStorage.getItem("selectedLang") || "tr";
function t(key) { return translations[currentLang][key] || key; }

function changeLanguage(lang) {
    currentLang = lang;
    localStorage.setItem("selectedLang", lang);
    
    // Statik elementleri çevir
    document.querySelectorAll("[data-i18n]").forEach(el => {
        const key = el.getAttribute("data-i18n");
        if (translations[lang][key]) {
            if (el.tagName === "INPUT") el.placeholder = translations[lang][key];
            else el.innerText = translations[lang][key];
        }
    });

    // Sayfaya özel dinamik listeleri/tabloları çevir
    if (typeof fetchProducts === "function") fetchProducts();
    if (typeof updateCartUI === "function") updateCartUI();
    if (typeof fetchDashboardData === "function") fetchDashboardData();
    if (typeof fetchOrders === "function") fetchOrders();
    
    const placeOrderBtn = document.getElementById("place-order-btn");
    if(placeOrderBtn) placeOrderBtn.innerText = t("place_order");
}

document.addEventListener("DOMContentLoaded", () => {
    const langSelect = document.getElementById("language-select");
    if(langSelect) langSelect.value = currentLang;
    changeLanguage(currentLang); 
    if (typeof checkSession === "function") checkSession();
});

// ==========================================
// 🟢 GLOBAL: DB'DEN GELEN METİNLERİ ÇEVİRME ARACI
// Patron ve Barista panellerinde kullanılır
// ==========================================
function formatCoffeeDetails(coffeeTypeStr) {
    if (!coffeeTypeStr) return `<li>${t('not_spec')}</li>`;
    return coffeeTypeStr.split(' | ').map(c => {
        let match = c.match(/(.+?)\s*\((.+)\)/);
        if (match) {
            let name = match[1];
            let details = match[2];
            // DB'den gelen virgüllü anahtarları (örn: size_m, temp_hot) ayır ve çevir
            let translatedDetails = details.split(',').map(d => t(d.trim())).join(', ');
            return `<li>${name} (${translatedDetails})</li>`;
        }
        return `<li>${c}</li>`; 
    }).join("");
}

// ==========================================
// 🔄 PUAN VE OTURUM YÖNETİMİ
// ==========================================
async function checkSession() {
    const savedUser = localStorage.getItem("cyberUser");
    if (savedUser) {
        const user = JSON.parse(savedUser);
        const authEl = document.getElementById("auth-panel");
        const profileEl = document.getElementById("user-profile");
        if(authEl) authEl.style.display = "none";
        if(profileEl) profileEl.style.display = "block";
        document.getElementById("display-name").innerText = currentLang === 'en' ? `Hi, ${user.username}!` : `Merhaba, ${user.username}!`;
        
        const pointsEl = document.getElementById("display-points");
        if(pointsEl) pointsEl.innerText = user.loyaltyPoints;

        try {
            const res = await fetch(`${API_URL}/Customer/get-points/${user.username}`);
            if (res.ok) {
                const updatedUser = await res.json();
                if(pointsEl) pointsEl.innerText = updatedUser.loyaltyPoints;
                localStorage.setItem("cyberUser", JSON.stringify(updatedUser));
            }
        } catch (e) {}
    }
}

async function refreshPoints() {
    const savedUser = localStorage.getItem("cyberUser");
    if (!savedUser) return;
    const user = JSON.parse(savedUser);
    try {
        const res = await fetch(`${API_URL}/Customer/get-points/${user.username}`);
        if (res.ok) {
            const updated = await res.json();
            localStorage.setItem("cyberUser", JSON.stringify(updated));
            const pointsEl = document.getElementById("display-points");
            if(pointsEl) pointsEl.innerText = updated.loyaltyPoints;
        }
    } catch (e) {}
}

function logout() { localStorage.removeItem("cyberUser"); location.reload(); }

// ==========================================
// ☕ MENÜ VE MODAL MANTIĞI
// ==========================================
async function fetchProducts() {
    const container = document.getElementById("menu-container");
    if(!container) return;
    container.innerHTML = `<p style="text-align:center; width:100%; color:#aaa; font-weight:bold;">${t('loading_menu')}</p>`;
    
    try {
        const res = await fetch(`${API_URL}/Product/get-products`);
        const products = await res.json();
        container.innerHTML = products.map(p => {
            const safeName = p.name.replace(/'/g, "&#39;");
            return `
            <div class="product-card">
                <img src="${p.imageUrl || 'https://cdn-icons-png.flaticon.com/512/924/924514.png'}">
                <h4>${p.name}</h4>
                <p>${p.price} TL</p>
                <button onclick="openModal(${p.id}, '${safeName}', ${p.price})">${t('btn_select')}</button>
            </div>`;
        }).join("");
    } catch { container.innerHTML = `<p style="text-align:center; width:100%; color:red;">${t('menu_error')}</p>`; }
}

function openModal(id, name, price) {
    isModalOpen = false;
    currentProduct = { id, name, basePrice: price, price: price };
    document.getElementById("modal-title").innerText = `☕ ${name}`;
    
    // Varsayılan ANAHTARLARA döner
    document.getElementById("modal-size").value = "size_s";
    document.getElementById("modal-size").disabled = false;
    document.getElementById("modal-concept").value = "temp_hot";
    document.getElementById("modal-syrup").value = "syr_none";
    document.getElementById("modal-extras").value = "ext_none";

    const user = JSON.parse(localStorage.getItem("cyberUser") || "null");
    const hasFree = cart.some(item => item.isFree);
    const rewardDiv = document.getElementById("modal-reward-container");
    if(rewardDiv) rewardDiv.style.display = (user && user.loyaltyPoints >= 10 && !hasFree) ? "block" : "none";
    
    const cb = document.getElementById("modal-use-points");
    if(cb) cb.checked = false;

    calcProductPrice(); 
    document.getElementById("product-modal").style.display = "flex";
    setTimeout(() => { isModalOpen = true; }, 500);
}

function closeModal() { document.getElementById("product-modal").style.display = "none"; }

function toggleFreeCoffee() {
    const cb = document.getElementById("modal-use-points");
    const sizeSelect = document.getElementById("modal-size");
    if(cb && cb.checked) {
        sizeSelect.value = "size_s"; sizeSelect.disabled = true;
    } else {
        sizeSelect.value = "size_s"; sizeSelect.disabled = false;
    }
    calcProductPrice();
}

function calcProductPrice() {
    if (!currentProduct) return;
    const cb = document.getElementById("modal-use-points");
    const isFree = cb && cb.checked;
    if (isFree) { document.getElementById("product-price").innerText = "0"; return 0; }

    let total = currentProduct.basePrice;
    const size = document.getElementById("modal-size").value;
    const extras = document.getElementById("modal-extras").value;

    if (size === "size_m") total += 10;
    if (size === "size_l") total += 15;
    if (extras === "ext_milk") total += 10;
    if (extras === "ext_shot") total += 20;

    document.getElementById("product-price").innerText = total;
    return total;
}

function confirmAddToCart() {
    if (!isModalOpen) return;
    const size = document.getElementById("modal-size").value;
    const extras = document.getElementById("modal-extras").value;
    const cb = document.getElementById("modal-use-points");
    const isFree = cb && cb.checked;
    
    const finalPrice = calcProductPrice();

    cart.push({ 
        ...currentProduct, price: finalPrice, isFree: isFree, 
        size: isFree ? "size_s" : size, 
        concept: document.getElementById("modal-concept").value, 
        syrup: document.getElementById("modal-syrup").value, 
        extras: extras
    });
    
    closeModal(); updateCartUI();
}

function updateCartUI() {
    const itemsDiv = document.getElementById("cart-items");
    const totalEl = document.getElementById("total-price");
    if(!itemsDiv) return;

    if (cart.length === 0) {
        itemsDiv.innerHTML = `<p style='text-align:center; color:gray; padding:15px;'>${t('empty_cart')}</p>`;
        totalEl.innerText = "0"; return;
    }
    
    itemsDiv.innerHTML = cart.map((item, i) => {
        let detailsArr = [];
        if (item.id === 999) {
            detailsArr.push(`${item.shots} Shot`);
            detailsArr.push(t(item.extras));
            detailsArr.push(t(item.concept));
            if (item.syrup !== "syr_none") { item.syrup.split("+").forEach(s => detailsArr.push(t(s))); }
        } else {
            let sizeWithPrice = t(item.size);
            if (!item.isFree) {
                if (item.size === "size_m") sizeWithPrice += " (+10 TL)";
                if (item.size === "size_l") sizeWithPrice += " (+15 TL)";
            }
            detailsArr.push(sizeWithPrice);
            detailsArr.push(t(item.concept));
            if (item.syrup && item.syrup !== "syr_none") detailsArr.push(t(item.syrup));
            if (item.extras && item.extras !== "ext_none") detailsArr.push(t(item.extras));
        }

        return `
        <div class="cart-item">
            <div class="cart-item-header">
                <span class="cart-item-name">${item.id === 999 ? '🧪' : '☕'} ${item.name}</span>
                <span>${item.isFree ? '🎁' : item.price + ' TL'} <button onclick="cart.splice(${i},1); updateCartUI()" style="color:red; background:none; border:none; cursor:pointer; font-weight:bold; margin-left:10px;">X</button></span>
            </div>
            <div class="cart-item-details">${detailsArr.join(", ")}</div>
        </div>`;
    }).join("");
    
    totalEl.innerText = cart.reduce((sum, item) => sum + item.price, 0);
}

// ==========================================
// 🧪 MIXOLOGY LAB
// ==========================================
function openMixologyLab() {
    isModalOpen = false;
    document.getElementById("mix-shot").value = "1";
    document.getElementById("mix-milk").value = "mix_m_none";
    document.getElementById("mix-temp").value = "temp_hot";
    document.querySelectorAll('.mix-syrup').forEach(cb => cb.checked = false);
    calcMixPrice();
    document.getElementById("mixology-modal").style.display = "flex";
    setTimeout(() => { isModalOpen = true; }, 500);
}

function closeMixologyLab() { document.getElementById("mixology-modal").style.display = "none"; }

function calcMixPrice() {
    const shots = parseInt(document.getElementById("mix-shot").value) || 1;
    const milk = document.getElementById("mix-milk");
    const milkPrice = parseInt(milk.options[milk.selectedIndex].dataset.price || 0);
    const syrups = document.querySelectorAll('.mix-syrup:checked').length;
    const total = (shots * 20) + milkPrice + (syrups * 15);
    const mixPriceEl = document.getElementById("mix-price");
    if(mixPriceEl) mixPriceEl.innerText = total;
    return total;
}

function addMixologyToCart() {
    if (!isModalOpen) return;
    const shots = document.getElementById("mix-shot").value;
    const milk = document.getElementById("mix-milk").value;
    const temp = document.getElementById("mix-temp").value;
    const syrupsArray = Array.from(document.querySelectorAll('.mix-syrup:checked')).map(cb => cb.value);
    
    cart.push({
        id: 999, name: "🧪 Mixology", price: calcMixPrice(),
        isFree: false, size: "size_special", concept: temp, shots: shots, 
        syrup: syrupsArray.join("+") || "syr_none", extras: milk
    });
    closeMixologyLab(); updateCartUI();
}

// ==========================================
// 🚀 SİPARİŞ VE TAKİP (API'YE ANAHTAR GÖNDERİMİ)
// ==========================================
async function placeOrder() {
    const nameEl = document.getElementById("customer-name");
    if(!nameEl) return;
    const name = nameEl.value.trim();
    if(!name || cart.length === 0) return alert(t('alert_empty'));
    
    const audio = document.getElementById("status-sound");
    if(audio) { audio.load(); }

    const btn = document.getElementById("place-order-btn");
    btn.disabled = true; btn.innerText = t('btn_sending');
    
    const savedUser = localStorage.getItem("cyberUser");
    const user = savedUser ? JSON.parse(savedUser) : { username: "" };

    const paymentVal = document.getElementById("order-payment").value;

    const orderData = {
        customerName: name,
        customerUsername: user.username,
        totalPrice: cart.reduce((sum, i) => sum + i.price, 0),
        UsedPoints: cart.some(i => i.isFree),
        coffeeType: cart.map(i => {
            let detailKeys = [];
            if (i.id === 999) {
                detailKeys.push(`${i.shots} Shot`);
                detailKeys.push(i.extras);
                detailKeys.push(i.concept);
                if (i.syrup !== "syr_none") i.syrup.split("+").forEach(s => detailKeys.push(s));
            } else {
                detailKeys.push(i.size);
                detailKeys.push(i.concept);
                if (i.syrup !== "syr_none") detailKeys.push(i.syrup);
                if (i.extras !== "ext_none") detailKeys.push(i.extras);
            }
            return `${i.name} (${detailKeys.join(", ")})`;
        }).join(" | "),
        status: "Waiting",
        paymentMethod: paymentVal === "Nakit" || paymentVal === "Cash" ? "Nakit" : "Kredi Kartı"
    };

    try {
        const res = await fetch(`${API_URL}/Order/place-order`, {
            method: 'POST', headers: {'Content-Type': 'application/json'}, body: JSON.stringify(orderData)
        });
        if(res.ok) {
            alert(t('alert_success'));
            startOrderTracking(name);
            cart = []; updateCartUI();
            nameEl.value = "";
            setTimeout(refreshPoints, 2000);
        } else {
            alert("Error placing order!");
        }
    } catch { alert("Error!"); }
    finally { btn.disabled = false; btn.innerText = t('place_order'); }
}

function startOrderTracking(name) {
    const tracker = document.getElementById("order-tracker");
    const statusEl = document.getElementById("tracker-status");
    const audio = document.getElementById("status-sound");
    if(!tracker || !statusEl) return;

    tracker.style.display = "block"; lastStatus = "";
    if(trackingInterval) clearInterval(trackingInterval);
    trackingInterval = setInterval(async () => {
        try {
            const res = await fetch(`${API_URL}/Order/get-orders`);
            const orders = await res.json();
            const myOrder = orders.sort((a,b)=>b.id-a.id).find(o => o.customerName.toLowerCase() === name.toLowerCase());
            if(myOrder) {
                const currentStatus = myOrder.status;

                if (lastStatus !== "" && lastStatus !== currentStatus) {
                    if (currentStatus === "Preparing" || currentStatus === "Ready") {
                        if(audio) { audio.currentTime = 0; audio.play().catch(() => {}); }
                    }
                }

                lastStatus = currentStatus;
                
                let displayStatus = t('status_waiting');
                if(currentStatus === "Preparing") displayStatus = t('status_preparing');
                if(currentStatus === "Ready") displayStatus = t('status_ready');
                if(currentStatus === "Delivered") displayStatus = t('status_delivered');
                
                statusEl.innerText = displayStatus;

                if(currentStatus === "Delivered") {
                    clearInterval(trackingInterval);
                    setTimeout(() => { tracker.style.display = "none"; }, 5000);
                    refreshPoints();
                }
            }
        } catch {}
    }, 5000);
}

// ==========================================
// 👔 ADMİN PANELİ: CİRO VE Z RAPORU EKLENTİSİ
// ==========================================
async function ciroHesapla() {
    const ciroToplamEl = document.getElementById("total-revenue");
    if (!ciroToplamEl) return;

    try {
        const res = await fetch(`${API_URL}/Order/get-orders`);
        const orders = await res.json();

        let toplamCiro = 0, nakitCiro = 0, krediCiro = 0;

        orders.filter(o => o.isClosed === false).forEach(order => {
            toplamCiro += order.totalPrice;
            if (order.paymentMethod === "Kredi Kartı" || order.paymentMethod === "Credit Card") { 
                krediCiro += order.totalPrice; 
            } else { 
                nakitCiro += order.totalPrice; 
            }
        });

        ciroToplamEl.innerText = toplamCiro.toLocaleString('tr-TR') + " TL";
        const nakitEl = document.getElementById("cash-revenue");
        const krediEl = document.getElementById("credit-revenue");
        if (nakitEl) nakitEl.innerText = nakitCiro.toLocaleString('tr-TR') + " TL";
        if (krediEl) krediEl.innerText = krediCiro.toLocaleString('tr-TR') + " TL";

    } catch (e) { }
}

async function takeZReport() {
    if(!confirm(t('z_conf'))) return;
    try {
        const res = await fetch(`${API_URL}/Order/take-z-report`, { method: 'POST' });
        if(res.ok) { alert(t('z_succ')); location.reload(); }
    } catch (e) { alert("Hata!"); }
}