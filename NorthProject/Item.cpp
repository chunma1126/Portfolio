#include "pch.h"
#include "Item.h"
#include "Texture.h"
#include "Collider.h"
#include "ResourceManager.h"
#include "EventManager.h"
#include "TimeManager.h"
Item::Item()
{
	m_pTexture = GET_SINGLE(ResourceManager)->TextureLoad(L"Item" , L"Texture\\Item.bmp");
	SetSize({ 50,80 });
	SetTag(TagEnum::Item);

	AddComponent<Collider>();
	GetComponent<Collider>()->SetSize({ 40,40 });
	GetComponent<Collider>()->SetOffSetPos({  12,20});

}

Item::~Item()
{
}

void Item::Update()
{
	Vec2 curPos = GetPos();
	curPos.y += 100 * fDT;
	SetPos(curPos);

	if (m_vPos.y < -m_vSize.y || m_vPos.y > SCREEN_HEIGHT + m_vSize.y ||
		m_vPos.x < -m_vSize.x || m_vPos.x > SCREEN_WIDTH + m_vSize.x)
	{
		GET_SINGLE(EventManager)->DeleteObject(this);
	}

}

void Item::Render(HDC _hdc)
{

	Vec2 vPos = GetPos();
	Vec2 vSize = GetSize();

	int width = m_pTexture->GetWidth();
	int height = m_pTexture->GetHeight();

	::TransparentBlt(_hdc
		, (int)(vPos.x - width / 2)
		, (int)(vPos.y - height / 2)
		, width + vSize.x / 2, height + vSize.y / 2,
		m_pTexture->GetTexDC()
		, 0, 0, width, height, RGB(255, 0, 255));
	ComponentRender(_hdc);
}
