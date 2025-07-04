# North Project

<aside>

> 📌 동방 프로젝트에서 영감을 받은 슈팅 게임으로, 다양한 탄막 패턴을 피해 보스를 처치하는 것이 목표입니다.

</aside>

<p align="left">
  <img src="Image/스크린샷_1.png" alt="NorthProject 이미지1" width="250"/>
  <img src="Image/스크린샷_2.png" alt="NorthProject 이미지2" width="250"/>
  <img src="Image/스크린샷_3.png" alt="NorthProject 이미지3" width="250"/>
</p>

🔗 [유튜브](https://youtu.be/tJJwyJQIKWo)  
🔗 [문서](https://abaft-yarn-52e.notion.site/NortProject-1d5c32f25528804aab63d155d69cf811?pvs=74)  

| 항목 | 내용 |
| --- | --- |
| 🎮 게임 이름 | **North Project** |
| 🕹 장르 | 탄막, 슈팅 |
| 🛠 사용 기술 | WinAPI, C++ |
| 👤 역할 | 팀장 |
| 📅 개발 기간 | 2024.11.11 ~ 2024.12.06 |
| 👥 개발 인원 | 개발 2명, 기획 1명 |

## ✅ 수행한 역할

### 🔹 시스템 개발
- `BulletManager`를 개발하여 다양한 탄막 패턴을 손쉽게 조합하고 구현 가능하도록 설계.
- `UIManager`를 통해 UI 요소들을 중앙 집중식으로 관리.
- 공통 `UI` 클래스를 제작하여 모든 UI가 이를 상속받도록 하여 객체지향적으로 구조화.

### 🔹 콘텐츠 개발
- 용도에 맞는 다양한 버튼 및 UI 요소 직접 제작.
- 보스 및 적에 맞는 탄막 패턴 직접 설계 및 구현.
- 적 사망 시 이펙트 처리 및 플레이어 피격 시 흔들림 구현.
- 레벨업 아이템 제작 및 각 레벨별 효과 시스템 개발.
- 씬별 UI 배치 및 설정, 게임오버 및 재시작 기능 포함.

### 🔹 기타 시스템
- 무한 스크롤링 배경 구현.
- 전체 프로젝트 일정 관리 및 팀 기획 총괄.

---
## 🔹 주요 시스템 구성

### 탄막 관리 시스템
- `BulletManager.cs`를 통해 `BasicShot`, `CircleShot`, `CircleShotGoToTarget`, `ShapeShot`, `SpinShot`, `HeartShot`, `HeartShotGoToTarget`, `RoseShot`, `RoseSpinShot` 등 **총 9가지 탄막 패턴을 구현**하고, 이 패턴들을 자유롭게 조합할 수 있도록 유연하게 설계했습니다.

- `Projectile.cpp`에서는 각 탄환이 **자체 조건에 따라 스스로 파괴되도록** 설계하여, 관리 비용을 줄이고 독립적인 동작이 가능하게 만들었습니다.  
- `Projectile`을 상속받아 `EnemyProjectile`, `FollowProjectile` 등 다양한 특성의 탄환을 구현했습니다.

### UI
- `UI.cpp`를 통해 UI 요소들을 **상속 구조**로 개발하여 재사용성과 확장성을 높였습니다.  
- `UIManager.cpp`를 싱글톤 패턴으로 설계해 전체 UI 흐름을 통합적으로 관리했습니다.  
- `Button.cpp`에서는 버튼의 상태(Enter, Hover, Click, Exit)에 따라 **다양한 이벤트를 처리할 수 있도록 구조화**했습니다.

### 씬 관리
- `TitleScene`, `GameScene`, `BossScene` 등을 초기화하고, **ReStart 시 각 씬 내 오브젝트를 정리하도록 구현**해 재시작 시 깔끔한 상태로 돌아갈 수 있도록 만들었습니다.

### 무한 스크롤링
- `Background.cpp`를 이용해 **배경이 끊김 없이 반복되는 무한 스크롤링 효과**를 구현했습니다.