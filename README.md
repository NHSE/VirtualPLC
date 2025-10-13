# ⚡ VirtualPLC

> **반도체 장비 프로그램과 연동하여 PLC 기반 모터 제어를 시뮬레이션하는 프로젝트입니다.**  
> 기존의 딜레이 기반 모터 제어를 Virtual PLC 통신 방식으로 대체하여,  
> Clean 및 Dry 공정에서 **웨이퍼 Spin 동작을 실제 장비와 유사하게 재현**합니다.  
> 이를 통해 **공정 타이밍과 모터 RPM 제어를 정확하게 검증**하고,  
> 실제 장비 없이도 장비 제어 로직을 테스트할 수 있습니다.

---

<p align="center">
  <img src="https://github.com/user-attachments/assets/489305de-303d-4622-a771-e963b880b4fd" alt="PLC" width="900"/>
</p>

---

## ⚙️ 사용 방법
🔗 [Simulator](https://github.com/NHSE/SemiConductor-Equipment/blob/master/docs/Simulator.md)

---

## 🏗️ Architecture

<p align="center">
  <img width="900" alt="Architecture" src="https://github.com/user-attachments/assets/61a4433e-205a-480d-ab2e-05f289a60507" />
</p>

---

## 🌐 통신 구조

### 💾 Coil

<p align="center">
  <img width="900" alt="Coil" src="https://github.com/user-attachments/assets/dc8ae3ce-4091-404c-a7d1-950e7cfc7365" />
</p>

| Address | Name | Note |
|:--------:|:-----|:-----|
| 0x0d | Master Connect | Master 연결 확인 |
| 0x0e | Slave Connect | Slave 연결 확인 |

---

### 💾 Registers

<p align="center">
  <img width="900" alt="Registers" src="https://github.com/user-attachments/assets/67e7bf0f-a3cc-4b66-9f75-17d45966b42f" />
</p>

| Address | Name | Note |
|:--------:|:------|:------|
| 0x01 | CleanChamber1 |  |
| 0x02 | CleanChamber2 |  |
| 0x03 | CleanChamber3 |  |
| 0x04 | CleanChamber4 |  |
| 0x05 | CleanChamber5 |  |
| 0x06 | CleanChamber6 |  |
| 0x07 | DryChamber1 |  |
| 0x08 | DryChamber2 |  |
| 0x09 | DryChamber3 |  |
| 0x0a | DryChamber4 |  |
| 0x0b | DryChamber5 |  |
| 0x0c | DryChamber6 |  |

---
