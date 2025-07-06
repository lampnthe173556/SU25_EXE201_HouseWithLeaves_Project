// Dữ liệu địa chỉ Việt Nam - Backup khi API gặp lỗi
// Cập nhật lần cuối: 2024

const VIETNAM_ADDRESS_DATA = {
  // Danh sách tỉnh/thành phố
  provinces: [
    { name: "Hà Nội", code: "01" },
    { name: "TP Hồ Chí Minh", code: "79" },
    { name: "Hải Phòng", code: "31" },
    { name: "Đà Nẵng", code: "48" },
    { name: "Cần Thơ", code: "92" },
    { name: "An Giang", code: "89" },
    { name: "Bà Rịa - Vũng Tàu", code: "77" },
    { name: "Bắc Giang", code: "24" },
    { name: "Bắc Kạn", code: "06" },
    { name: "Bạc Liêu", code: "95" },
    { name: "Bắc Ninh", code: "27" },
    { name: "Bến Tre", code: "83" },
    { name: "Bình Định", code: "52" },
    { name: "Bình Dương", code: "74" },
    { name: "Bình Phước", code: "70" },
    { name: "Bình Thuận", code: "60" },
    { name: "Cà Mau", code: "96" },
    { name: "Cao Bằng", code: "04" },
    { name: "Đắk Lắk", code: "33" },
    { name: "Đắk Nông", code: "67" },
    { name: "Điện Biên", code: "11" },
    { name: "Đồng Nai", code: "75" },
    { name: "Đồng Tháp", code: "87" },
    { name: "Gia Lai", code: "64" },
    { name: "Hà Giang", code: "02" },
    { name: "Hà Nam", code: "35" },
    { name: "Hà Tĩnh", code: "42" },
    { name: "Hải Dương", code: "30" },
    { name: "Hậu Giang", code: "93" },
    { name: "Hòa Bình", code: "17" },
    { name: "Hưng Yên", code: "33" },
    { name: "Khánh Hòa", code: "56" },
    { name: "Kiên Giang", code: "91" },
    { name: "Kon Tum", code: "62" },
    { name: "Lai Châu", code: "12" },
    { name: "Lâm Đồng", code: "68" },
    { name: "Lạng Sơn", code: "20" },
    { name: "Lào Cai", code: "10" },
    { name: "Long An", code: "80" },
    { name: "Nam Định", code: "36" },
    { name: "Nghệ An", code: "40" },
    { name: "Ninh Bình", code: "37" },
    { name: "Ninh Thuận", code: "58" },
    { name: "Phú Thọ", code: "25" },
    { name: "Phú Yên", code: "54" },
    { name: "Quảng Bình", code: "44" },
    { name: "Quảng Nam", code: "49" },
    { name: "Quảng Ngãi", code: "51" },
    { name: "Quảng Ninh", code: "22" },
    { name: "Quảng Trị", code: "45" },
    { name: "Sóc Trăng", code: "94" },
    { name: "Sơn La", code: "14" },
    { name: "Tây Ninh", code: "72" },
    { name: "Thái Bình", code: "34" },
    { name: "Thái Nguyên", code: "19" },
    { name: "Thanh Hóa", code: "38" },
    { name: "Thừa Thiên Huế", code: "46" },
    { name: "Tiền Giang", code: "82" },
    { name: "Trà Vinh", code: "84" },
    { name: "Tuyên Quang", code: "08" },
    { name: "Vĩnh Long", code: "86" },
    { name: "Vĩnh Phúc", code: "26" },
    { name: "Yên Bái", code: "15" }
  ],

  // Dữ liệu quận/huyện theo tỉnh (một số tỉnh chính)
  districts: {
    "01": [ // Hà Nội
      { name: "Ba Đình", code: "001" },
      { name: "Hoàn Kiếm", code: "002" },
      { name: "Tây Hồ", code: "003" },
      { name: "Long Biên", code: "004" },
      { name: "Cầu Giấy", code: "005" },
      { name: "Đống Đa", code: "006" },
      { name: "Hai Bà Trưng", code: "007" },
      { name: "Hoàng Mai", code: "008" },
      { name: "Thanh Xuân", code: "009" },
      { name: "Sóc Sơn", code: "016" },
      { name: "Đông Anh", code: "017" },
      { name: "Gia Lâm", code: "018" },
      { name: "Nam Từ Liêm", code: "019" },
      { name: "Thanh Trì", code: "020" },
      { name: "Bắc Từ Liêm", code: "021" },
      { name: "Mê Linh", code: "250" },
      { name: "Hà Đông", code: "268" },
      { name: "Sơn Tây", code: "269" },
      { name: "Ba Vì", code: "271" },
      { name: "Phúc Thọ", code: "272" },
      { name: "Đan Phượng", code: "273" },
      { name: "Hoài Đức", code: "274" },
      { name: "Quốc Oai", code: "275" },
      { name: "Thạch Thất", code: "276" },
      { name: "Chương Mỹ", code: "277" },
      { name: "Thanh Oai", code: "278" },
      { name: "Thường Tín", code: "279" },
      { name: "Phú Xuyên", code: "280" },
      { name: "Ứng Hòa", code: "281" },
      { name: "Mỹ Đức", code: "282" }
    ],
    "79": [ // TP Hồ Chí Minh
      { name: "Quận 1", code: "760" },
      { name: "Quận 12", code: "761" },
      { name: "Quận Thủ Đức", code: "762" },
      { name: "Quận 9", code: "763" },
      { name: "Quận Gò Vấp", code: "764" },
      { name: "Quận Bình Thạnh", code: "765" },
      { name: "Quận Tân Bình", code: "766" },
      { name: "Quận Tân Phú", code: "767" },
      { name: "Quận Phú Nhuận", code: "768" },
      { name: "Quận 2", code: "769" },
      { name: "Quận 3", code: "770" },
      { name: "Quận 10", code: "771" },
      { name: "Quận 11", code: "772" },
      { name: "Quận 4", code: "773" },
      { name: "Quận 5", code: "774" },
      { name: "Quận 6", code: "775" },
      { name: "Quận 8", code: "776" },
      { name: "Quận Bình Tân", code: "777" },
      { name: "Quận 7", code: "778" },
      { name: "Huyện Củ Chi", code: "783" },
      { name: "Huyện Hóc Môn", code: "784" },
      { name: "Huyện Bình Chánh", code: "785" },
      { name: "Huyện Nhà Bè", code: "786" },
      { name: "Huyện Cần Giờ", code: "787" }
    ],
    "48": [ // Đà Nẵng
      { name: "Quận Liên Chiểu", code: "490" },
      { name: "Quận Thanh Khê", code: "491" },
      { name: "Quận Hải Châu", code: "492" },
      { name: "Quận Sơn Trà", code: "493" },
      { name: "Quận Ngũ Hành Sơn", code: "494" },
      { name: "Quận Cẩm Lệ", code: "495" },
      { name: "Huyện Hòa Vang", code: "497" },
      { name: "Huyện Hoàng Sa", code: "498" }
    ]
  },

  // Dữ liệu phường/xã theo quận/huyện (một số quận chính)
  wards: {
    "001": [ // Ba Đình - Hà Nội
      { name: "Phường Phúc Xá", code: "00001" },
      { name: "Phường Trúc Bạch", code: "00004" },
      { name: "Phường Vĩnh Phúc", code: "00006" },
      { name: "Phường Cống Vị", code: "00007" },
      { name: "Phường Liễu Giai", code: "00008" },
      { name: "Phường Nguyễn Trung Trực", code: "00012" },
      { name: "Phường Quán Thánh", code: "00015" },
      { name: "Phường Ngọc Hà", code: "00016" },
      { name: "Phường Điện Biên", code: "00018" },
      { name: "Phường Đội Cấn", code: "00019" },
      { name: "Phường Ngọc Khánh", code: "00021" },
      { name: "Phường Kim Mã", code: "00022" },
      { name: "Phường Giảng Võ", code: "00024" },
      { name: "Phường Thành Công", code: "00025" }
    ],
    "760": [ // Quận 1 - TP HCM
      { name: "Phường Tân Định", code: "26734" },
      { name: "Phường Đa Kao", code: "26737" },
      { name: "Phường Bến Nghé", code: "26740" },
      { name: "Phường Bến Thành", code: "26743" },
      { name: "Phường Nguyễn Thái Bình", code: "26746" },
      { name: "Phường Phạm Ngũ Lão", code: "26749" },
      { name: "Phường Cầu Ông Lãnh", code: "26752" },
      { name: "Phường Cô Giang", code: "26755" },
      { name: "Phường Nguyễn Cư Trinh", code: "26758" },
      { name: "Phường Cầu Kho", code: "26761" }
    ],
    "490": [ // Liên Chiểu - Đà Nẵng
      { name: "Phường Hòa Hiệp Bắc", code: "20197" },
      { name: "Phường Hòa Hiệp Nam", code: "20198" },
      { name: "Phường Hòa Khánh Bắc", code: "20199" },
      { name: "Phường Hòa Khánh Nam", code: "20200" },
      { name: "Phường Hòa Minh", code: "20201" }
    ]
  }
};

// Hàm để lấy dữ liệu từ backup
function getProvincesFromBackup() {
  return VIETNAM_ADDRESS_DATA.provinces;
}

function getDistrictsFromBackup(provinceCode) {
  return VIETNAM_ADDRESS_DATA.districts[provinceCode] || [];
}

function getWardsFromBackup(districtCode) {
  return VIETNAM_ADDRESS_DATA.wards[districtCode] || [];
}

// Export để sử dụng trong các file khác
if (typeof module !== 'undefined' && module.exports) {
  module.exports = {
    VIETNAM_ADDRESS_DATA,
    getProvincesFromBackup,
    getDistrictsFromBackup,
    getWardsFromBackup
  };
} 