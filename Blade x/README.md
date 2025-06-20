# Blade X

<aside>

> 📌 하데스와 데스도어에서 영감을 받은, 매 도전마다 새롭게 성장하여 강력한 보스에 맞서는 로그라이크 게임입니다.

</aside>
<p align="left">
  <img src="Image\bladex_1.png" alt="{스크린샷1 설명}" width="250"/>
  <img src="Image\bladex_2.png" alt="{스크린샷2 설명}" width="250"/>
    <img src="Image\bladex_3.png" alt="{스크린샷2 설명}" width="250"/>
</p>

🔗 [유튜브](https://www.youtube.com/watch?v=NYQJKlHX2So)  
🔗 [문서](https://abaft-yarn-52e.notion.site/blade-x-1d5c32f25528803b96c3fd71d4189735)  

| 항목 | 내용 |
| --- | --- |
| 🎮 게임 이름 | **Blade X** |
| 🕹 장르 | 액션, 로그라이크 |
| 🛠 사용 기술 | Unity6, C# |
| 👤 역할 | **팀장** |
| 📅 개발 기간 | 2024.12.01 ~ 진행 중  |
| 👥 개발 인원 | 개발 5명 |

## ✅ 수행한 역할

### 🔹 시스템 개발
- Unity 6의 Behavior Tree를 활용한 일반 및 보스 Enemy AI 구현
- 전투 로직 구성: `Caster System`, `Health System`, `Spawn System` 설계 및 구현.
- Enemy 클래스 구조를 계층적으로 설계하여 재사용성 확보.

### 🔹 콘텐츠 개발
- 스킬을 ScriptableObject 기반으로 데이터화하여 유지보수성과 확장성 확보.  
- BaseEnemy를 상속받는 SwrodEnemy,GobliEnemy,BowEnemy 등등 게임에 등장하는 모든 적을 설계하고 구현함.

### 🔹 기타 시스템
- TagChangePopup을 구현하고 게임에 직접 적용함. 슬롯머신 느낌의 UI을 넣어 UX를 디자인함.

---

### 🔹 주요 시스템 구성

#### ✅ Enemy AI 시스템 (Behavior Tree 기반)
- Unity 6의 BT 시스템을 이용하여 일반 적, 보스 적의 패턴을 유연하게 구현
- 조건 및 행동 트리를 ScriptableObject로 구성하여 직관적인 설계 지원

#### ✅ 전투 시스템
- 체력 시스템, 공격 캐스팅, 리스폰 등의 핵심 전투 기능 모듈화
- 다양한 상황에 대응 가능한 유닛 스폰 로직 설계

#### ✅ 스킬 시스템
- 스킬 데이터를 SO로 설계하여, 수치 조정 및 밸런싱 작업을 쉽게 유지
- 공격 범위, 쿨타임, 이펙트 등을 데이터 기반으로 설정 가능

#### ✅ UI
- 전투 상황과 상태를 직관적으로 보여주는 전투 HUD 설계
