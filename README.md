# PlayMeow Login Demo

本專案示範兩套可擴充的 UI 登入頁面架構，分別採用  
**MVP (Model-View-Presenter)** 與 **MVVM (Model-View-ViewModel)** 模式，  
並搭配 **事件匯流閥 (Event Aggregator)** 或 **資料繫結 (Data Binding)** 進行溝通，達到低耦合與高擴充性的目的。

---

## 場景與版本

| 項目 | 值 |
|---|---|
| Demo Scene (MVP) | `Assets/Scenes/PlayMeowLoginDemo_MVP.unity` |
| Demo Scene (MVVM) | `Assets/Scenes/PlayMeowLoginDemo_MVVM.unity` |
| Unity Version | `2021.3.45f2` |

---

## 架構說明

### MVP 版本
- **View**：負責顯示 UI 與接收使用者操作，不處理邏輯。
- **Presenter**：負責邏輯與輸入驗證，並透過事件向 View 發送更新指令。
- **事件匯流閥**：用於 View ↔ Presenter 溝通，不需雙方持有引用，也無需建立 interface。

### MVVM 版本
- **View**：僅關注畫面呈現與 UI 元件。
- **ViewModel**：透過 `ObservableProperty<T>` 實現雙向資料繫結，管理輸入狀態、錯誤提示、顯示控制等。
- **Model**：負責資料結構與資料來源（如 API 或本地資料）。
- 無需事件匯流閥，改以 **資料繫結 (Binding)** 機制驅動 UI 更新，讓狀態變化能自動反映到畫面。

---

## MVP vs MVVM 比較

| 項目 | MVP | MVVM |
|------|-----|------|
| 架構溝通方式 | 事件匯流閥 (Event Aggregator) | 資料繫結 (ObservableProperty) |
| View 更新方式 | Presenter 呼叫 UICommand | 自動更新 (Binding 機制) |
| 可測試性 | Presenter 易測 | ViewModel 可單元測試化 |
| UI 維護成本 | 中等（需定義事件） | 低（屬性自動同步） |
| 擴充性 | 適合明確的流程導向邏輯 | 適合複雜狀態、資料驅動 UI |

---

## UI 資源載入與擴充性

UI 採用 **動態載入設計**。  
可於後續切換至 **Addressables** 或遠端資源，而不需修改 UI 邏輯，便於長期維護與更新。

---

## 語系與 Quality 切換

於 `PlayMeowSystem` → 右鍵選單 (Context Menu) 中可進行：

- **語系切換**：載入對應語言的圖片及文字。
- **Quality 切換**：替換不同品質的 UI 圖片資源。
- **測試 Presenter / ViewModel 指令**：方便檢查 UI 呈現流程行為。

---

## 平台輸入行為調整（iOS）

帳號輸入欄位未使用 `ContentType = Email`，以避免 iOS 手寫鍵盤在 Email 模式下可能出現無法正常輸入的問題。

---

## 邏輯與顯示分工原則

| 判斷 / 行為 | 所屬層 | 原因 |
|---|---|---|
| 帳號 / 密碼是否合法 | Presenter / ViewModel | 屬邏輯判斷 |
| 錯誤訊息呈現、UI 動畫 | View | 屬顯示層行為 |

---

## Summary

此專案提供兩種架構版本，可作為不同需求下的參考模板：

- **MVP**：事件導向、流程明確，適合傳統遊戲與互動流程控制。
- **MVVM**：資料導向、雙向繫結，適合工具類或狀態驅動 UI。
- **動態載入 + 多語系 / 品質切換**：可無痛整合 Addressables。
- **平台差異優化**：針對 iOS 鍵盤輸入進行特殊處理。

此專案可作為未來完整 UI Framework 的基礎模板，支援從小型 Demo 到正式產品架構的平滑擴充。
