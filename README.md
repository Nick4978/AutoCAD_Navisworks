# Navisworks CAD List Extractor

This is a standalone .NET Framework 4.8 desktop application that opens a Navisworks `.nwd` file and extracts model item data from it — including item names, categories, and associated properties.

You can filter the extracted data using standard LINQ `.Where()` expressions directly in code, and view the filtered results in a simple UI textbox.

---

## 🚀 Features

- 🔍 Load `.nwd` files using the Autodesk Navisworks API
- 🧱 Traverse model items and their properties
- 🧠 Filter model items with custom LINQ queries
- 🖥️ View results in a scrollable textbox for easy copying/exporting
- ✅ Built as a standalone WinForms app using .NET Framework 4.8

---

## 🛠 Requirements

- Windows 10 or later (x64)
- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- [Autodesk Navisworks Manage 2025](https://www.autodesk.com/products/navisworks/overview) installed  
  > ✅ Must be 64-bit — this app uses the Navisworks API directly from the installation directory.

---

## 📦 Setup

1. Clone this repo:

   ```bash
   git clone https://github.com/yourusername/navisworks-cad-list-extractor.git
