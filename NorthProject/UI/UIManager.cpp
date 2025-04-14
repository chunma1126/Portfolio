#include "pch.h"
#include "UIManager.h"
#include "SceneManager.h"
#include "ResourceManager.h"
#include "InputManager.h"
#include "UI.h"
#include "Button.h"
#include "PlayButton.h"
#include "ExitButton.h"
#include "RestartButton.h"
#include "EventManager.h"
#include "GDISelector.h"

void UIManager::Init()
{
	{
		UI* playerHeart = new UI;
		
		playerHeart->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"PlayerHeart" , L"Texture\\UI\\PlayerHeart.bmp"));
		playerHeart->SetPos({50 , 50});
		playerHeart->SetSize({75,75});
		AddChild(L"PlayerHeart1", playerHeart);
	}

	{
		UI* playerHeart = new UI;

		playerHeart->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"PlayerHeart", L"Texture\\UI\\PlayerHeart.bmp"));
		playerHeart->SetPos({ 100 , 50 });
		playerHeart->SetSize({ 75,75 });
		AddChild(L"PlayerHeart2", playerHeart);
	}

	//tutorialInfo
	{
		UI* showKeys = new UI;

		showKeys->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"ShowKeys", L"Texture\\UI\\ShowKeys.bmp"));
		showKeys->SetPos({ SCREEN_WIDTH /2 - 300 , SCREEN_HEIGHT - 200 });
		showKeys->SetSize({ 150,80 });
		
		AddChild(L"ShowKeys", showKeys);
	}

	{
		UI* moveKeys = new UI;

		moveKeys->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"MoveKeys", L"Texture\\UI\\ShowWASD.bmp"));
		moveKeys->SetPos({ SCREEN_WIDTH / 2 - 150 , SCREEN_HEIGHT - 200 });
		moveKeys->SetSize({ 150,80 });

		AddChild(L"MoveKeys", moveKeys);
	}


	{
		UI* playerHeart = new UI;

		playerHeart->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"PlayerHeart", L"Texture\\UI\\PlayerHeart.bmp"));
		playerHeart->SetPos({ 150 , 50 });
		playerHeart->SetSize({ 75,75 });
		AddChild(L"PlayerHeart3", playerHeart);
	}

	//Score
	{
		m_scoreUIs[0] = new UI;

		m_scoreUIs[0]->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"0", L"Texture\\Number\\0.bmp"));
		m_scoreUIs[0]->SetPos({ 140 , 90 });
		m_scoreUIs[0]->SetSize({ 75,75 });
		AddChild(L"FirstScore", m_scoreUIs[0]);
		
		m_scoreUIs[1] = new UI;

		m_scoreUIs[1]->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"0", L"Texture\\Number\\0.bmp"));
		m_scoreUIs[1]->SetPos({ 95 , 90 });
		m_scoreUIs[1]->SetSize({ 75,75 });
		AddChild(L"SecondeScore", m_scoreUIs[1]);

		m_scoreUIs[2] = new UI;

		m_scoreUIs[2]->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"0", L"Texture\\Number\\0.bmp"));
		m_scoreUIs[2]->SetPos({ 50 , 90 });
		m_scoreUIs[2]->SetSize({ 75,75 });
		AddChild(L"ThirdScore", m_scoreUIs[2]);
	}

	{
		UI* Clear = new UI;

		Clear->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"Clear", L"Texture\\UI\\Clear.bmp"));
		Clear->SetPos({ SCREEN_WIDTH / 2 - 120 , SCREEN_HEIGHT / 2 - 150 });
		Clear->SetSize({ 600,125 });

		AddChild(L"Clear", Clear);
	}

	{
		UI* Title = new UI;

		Title->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"Title", L"Texture\\UI\\Title2.bmp"));
		Title->SetPos({ 135, 200 });
		Title->SetSize({ 1100,200 });

		AddChild(L"Title", Title);
	}

	{
		UI* gameOver = new UI;

		gameOver->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"GameOver", L"Texture\\UI\\GameOver.bmp"));
		gameOver->SetSize({ 1350,150 });
		gameOver->SetPos({ SCREEN_WIDTH / 2 - 340 , SCREEN_HEIGHT / 2 - 150});
		AddChild(L"GameOver", gameOver);
		
	}

	{
		PlayButton* playButton = new PlayButton;
		playButton->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"PlayButton", L"Texture\\UI\\PlayButton.bmp"));
		playButton->SetPos({ SCREEN_WIDTH / 2 - 140  , SCREEN_HEIGHT / 2});
		playButton->SetSize({ 600,200 });

		Texture* hover = (GET_SINGLE(ResourceManager)->TextureLoad(L"PlayButtonHover", L"Texture\\UI\\PlayButtonHover.bmp"));
		playButton->SetHoverTexture(hover);

		Texture* press = (GET_SINGLE(ResourceManager)->TextureLoad(L"PlayButtonPress", L"Texture\\UI\\PlayButtonPress.bmp"));
		playButton->SetPressTexture(press);

		AddChild(L"PlayButton", playButton);
	}

	{
		ExitButton* exitButton = new ExitButton;
		exitButton->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"QuitButton", L"Texture\\UI\\QuitButton.bmp"));
		exitButton->SetPos({ SCREEN_WIDTH / 2 - 140 , SCREEN_HEIGHT / 2 + 130 });
		exitButton->SetSize({ 600,200 });

		Texture* hover = (GET_SINGLE(ResourceManager)->TextureLoad(L"QuitButtonHover", L"Texture\\UI\\QuitButtonHover.bmp"));
		exitButton->SetHoverTexture(hover);

		Texture* press = (GET_SINGLE(ResourceManager)->TextureLoad(L"QuitButtonPress", L"Texture\\UI\\QuitButtonPress.bmp"));
		exitButton->SetHoverTexture(hover);
		exitButton->SetPressTexture(press);

		AddChild(L"ExitButton", exitButton);
	}

	{
		RestartButton* restartButton = new RestartButton;
		restartButton->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"RestartButton", L"Texture\\UI\\RestartButton.bmp"));
		restartButton->SetPos({ SCREEN_WIDTH / 2 - 140  , SCREEN_HEIGHT / 2 +50 });
		restartButton->SetSize({ 600,200 });

		Texture* hover = (GET_SINGLE(ResourceManager)->TextureLoad(L"RestartButtonHover", L"Texture\\UI\\RestartButtonHover.bmp"));
		restartButton->SetHoverTexture(hover);

		Texture* press = (GET_SINGLE(ResourceManager)->TextureLoad(L"RestartButtonPress", L"Texture\\UI\\RestartButtonPress.bmp"));
		restartButton->SetPressTexture(press);

		AddChild(L"RestartButton", restartButton);

	}

	

	for (auto& item : uiLists)
	{
		item.second->Init();
	}
	
}

void UIManager::Update()
{
	for (auto& item : uiLists)
	{
		item.second->Update();
	}
}

void UIManager::Render(HDC _hdc)
{
	for (auto& item : uiLists)
	{
		item.second->Render(_hdc);
	}
}

void UIManager::Release()
{
	map<wstring, UI*>::iterator iter;
	for (iter = uiLists.begin(); iter != uiLists.end(); ++iter) {
		if (iter->second != nullptr)
			delete iter->second;
	}
	uiLists.clear();
}

void UIManager::AddChild(wstring _key,UI* _newUI)
{
	uiLists[_key] = _newUI;
}

void UIManager::SetActiveChild(wstring _key, bool _active)
{
	if (uiLists.find(_key) == uiLists.end())
	{
		return;
	}

	if (uiLists[_key] == nullptr)
	{
		return;
	}

	uiLists[_key]->SetActive(_active);
	
}

void UIManager::ChangeScore()
{
	int currentScore = m_gameScore;

	int index = 0;
	
	while (currentScore > 1)
	{
		m_gameTotalScores[index++] += currentScore % 10;
		currentScore /= 10;
	}

	m_gameScore = 0;

	for (int i = 0; i < 2; i++)
	{
		if (m_gameTotalScores[i] >= 10) 
		{
			m_gameTotalScores[i] %= 10;
			m_gameTotalScores[i + 1] += 1;
		}
	}

	if (m_gameTotalScores[2] >= 10)
	{
		for (int i = 0; i < 3; i++)
		{
			m_gameTotalScores[i] = 9;
		}
	}

	for (int i = 2; i >= 0; i--)
	{	
		wstring number = std::to_wstring(m_gameTotalScores[i]);
		m_scoreUIs[i]->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(number, L"Texture\\Number\\" + number + L".bmp"));
	}

}

void UIManager::ResetScore()
{
	for (int i = 0; i < 3; i++)
	{
		m_gameTotalScores[i] = 0;
	}

	for (int i = 2; i >= 0; i--)
	{
		m_scoreUIs[i]->SetTexture(GET_SINGLE(ResourceManager)->TextureLoad(L"0", L"Texture\\Number\\0.bmp"));
	}
}

void UIManager::SetPosChild(wstring _key, Vec2 _pos)
{
	uiLists[_key]->SetPos(_pos);
}

void UIManager::OnCompleteBossScene()
{
	SetActiveChild(L"Clear",true);
	SetActiveChild(L"RestartButton", true);
}
