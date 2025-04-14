#pragma once
class UI;
class UIManager
{
	DECLARE_SINGLE(UIManager);

public:
	void Init();
	void Update();
	void Render(HDC _hdc);
	void Release();
public :
	void AddChild(wstring _key,UI* _newUI);
	void SetActiveChild(wstring _key , bool _active);
	void SetPosChild(wstring _key , Vec2 _pos);
	void OnCompleteBossScene();
	void ChangeScore();
	void ResetScore();
	void AddScore(float _value)
	{
		m_gameScore += _value;
	
		ChangeScore();
	};

private :
	std::map<wstring, UI*> uiLists;

	int m_gameScore = 0;
	int m_gameTotalScores[3] = {0,0,0};

	UI* m_scoreUIs[3];

	
};

