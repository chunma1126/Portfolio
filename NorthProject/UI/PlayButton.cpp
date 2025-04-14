#include "pch.h"
#include "PlayButton.h"
#include "SceneManager.h"
#include "UIManager.h"
#include "ResourceManager.h"
void PlayButton::ClickEvent()
{
	if (!m_active)return;



	GET_SINGLE(UIManager)->SetActiveChild(L"Title", false);
	GET_SINGLE(UIManager)->SetActiveChild(L"PlayButton", false);
	GET_SINGLE(UIManager)->SetActiveChild(L"ExitButton", false);
	GET_SINGLE(UIManager)->SetActiveChild(L"ShowKeys", false);
	GET_SINGLE(UIManager)->SetActiveChild(L"MoveKeys", false);



	GET_SINGLE(ResourceManager)->Stop(SOUND_CHANNEL::BGM);
	GET_SINGLE(ResourceManager)->LoadSound(L"InGameBGM",L"Sound\\InGameBGM.mp3",true);
	GET_SINGLE(ResourceManager)->PlayAudio(L"InGameBGM");


	GET_SINGLE(SceneManager)->LoadScene(L"BossScene");
}
