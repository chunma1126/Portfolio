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

## 수행한 역할

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

## 🔹 주요 시스템 구성

### Enemy AI 시스템 (Behavior Tree 기반)

- `BaseEnemy.cs`, `BaseEnemyAnimatorController.cs`, `BaseEnemyHealth.cs` 등을 통해 적 캐릭터의 공통 로직을 추상화하여 관리했습니다.  
- Unity 6의 **Behavior Tree 시스템**을 활용해 적의 행동과 애니메이션을 분리하여 구현함으로써 **로직 간 의존성을 줄이고 유지보수성을 높일 수 있었습니다**.  
- 이 구조 덕분에 AI 로직을 빠르게 테스트하거나 새로운 행동을 유연하게 추가할 수 있었습니다.

---

### 전투 시스템

- `ICasterable.cs`를 도입하여 플레이어와 적의 **캐스팅 로직을 통합적으로 추상화**함으로써 서로 다른 객체 간 전투 시스템을 일관되게 구현했습니다.  
- `ActionData` 구조체를 통해 전투 중 발생하는 데이터를 표준화하여, **데이터 전달과 디버깅을 간편하게 만들었습니다**.  
- 체력 시스템은 `IHealth` 인터페이스를 기반으로 `EntityHealth.cs`, `PlayerHealth.cs`, `EnemyHealth.cs` 등으로 확장해 **모든 유닛의 생명 관련 처리를 일관성 있게 유지할 수 있었습니다**.

---

### 스킬 시스템

- `SkillDataSO` 추상 클래스와 Scriptable Object 기반의 설계를 통해 **스킬을 데이터 중심으로 관리할 수 있도록 구조화**했습니다.  
- 스킬 로직은 `PlayerSkillController.cs`에서 **중앙 집중식으로 제어**하여, **스킬 간 충돌 방지와 확장성 있는 설계가 가능**하도록 만들었습니다.  
- 덕분에 새로운 스킬을 추가하거나 밸런스를 조정할 때 코드 수정 없이도 데이터만으로 쉽게 조정할 수 있었습니다.

---

### 레벨 시스템

- `Spawner` 추상 클래스를 중심으로 다양한 적 스포너들을 구현하여 **유닛 생성 로직의 재사용성과 테스트 용이성을 높였습니다**.  
- 레벨 구성은 `NodeDictionary`라는 자료구조를 만들어 각 노드(스테이지)를 체계적으로 관리할 수 있게 했습니다.  
- Enemy와 Spawner를 독립적으로 운용함으로써 **특정 상황에서 개별 테스트가 가능**해졌고, **디버깅 효율도 함께 향상**되었습니다.


