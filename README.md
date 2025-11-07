# PlayMeow Login Demo

本專案示範一套可擴充的 UI 登入頁面架構，採用 **MVP (Model-View-Presenter)** 並搭配 **事件匯流閥 (Event Aggregator)** 進行溝通，達到低耦合與易維護的目的。

---
## 場景與版本

| 項目 | 值 |
|---|---|
| Demo Scene | `Assets/Scenes/PlayMeowLoginDemo.unity` |
| Unity Version | `2021.3.45f2` |

---
## 架構說明

- **View**：負責顯示 UI 與接收使用者操作，不處理邏輯。
- **Presenter**：負責邏輯與輸入驗證，並透過事件向 View 發送更新指令。
- **事件匯流閥** 用於 View ↔ Presenter 溝通：
  - 不需要雙方持有引用
  - 不需額外建立 interface
  - UI 元件可自由替換，架構可平行擴充

---

## UI 資源載入與擴充性

UI 採用 **動態載入** 設計。  
此設計可在後續直接切換至 **Addressables** 或遠端資源，而不需改動 UI 邏輯，便於長期維護與更新。

---

## 語系與 Quality 切換

在 `PlayMeowSystem` → 右鍵選單(Context Menu)中可進行：

- **語系切換**：載入對應語言之圖片及文字
- **Quality 切換**：替換不同品質的 UI 圖片資源
- **測試 Presenter 指令**：方便檢查 UI 呈現流程行為

---

## 平台輸入行為調整（iOS）

帳號輸入欄位未使用 `ContentType = Email`，以避免 iOS 手寫鍵盤在 Email 模式下可能出現無法正常輸入的問題。

---

## 邏輯與顯示分工原則

| 判斷 / 行為 | 所屬層 | 原因 |
|---|---|---|
| 帳號/密碼是否合法 | Presenter | 屬邏輯判斷 |
| 錯誤訊息呈現、UI 動畫 | View | 屬顯示層行為 |

---

## Summary

此專案重點在於：

- UI 與邏輯分離、低耦合架構
- 可被後續資源系統（如 Addressables）無痛接管
- 支援語系與資源品質切換
- 顧及平台差異的輸入體驗細節

可作為後續擴充完整 UI Framework 的基礎模板。
