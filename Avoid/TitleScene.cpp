#include "TitleScene.h"
#include "ArrowPool.h"
#include "GameScene.h"
USING_NS_CC;

Scene* TitleScene::createScene()
{
    return TitleScene::create();
}

bool TitleScene::init()
{
    if (!Scene::init()) 
    {
        return false;
    }

    auto visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    
    //title Label
    {
        Vec2 pos = Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y - 75);

        auto titleLabel = Label::createWithSystemFont("press any key", "fonts/CookieRun Regular.ttf", 22);
        titleLabel->setPosition(pos);

        auto fadeIn = FadeTo::create(_titleLabelFadeInTime, _fadeInAlpha);
        auto fadeOut = FadeTo::create(_titleLabelFadeOutTime, _fadeOutAlpha);
        auto fadeSeq = Sequence::create(fadeIn, fadeOut, nullptr);
        auto repeatFade = RepeatForever::create(fadeSeq);

        titleLabel->runAction(repeatFade);
        addChild(titleLabel);
    }
    
    //title 
    {
        Vec2 pos = Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y + 75);
        auto title = Label::createWithSystemFont("AVOID", "fonts/CookieRun Regular.ttf", 95);
        title->setPosition(pos);

        addChild(title);
    }

    return true;
}

void TitleScene::onEnter()
{
    Scene::onEnter();

    CCLOG("Enter!!!!!!!!!!!!!!");

    auto keyboardEvent = EventListenerKeyboard::create();

    keyboardEvent->onKeyPressed = [&](EventKeyboard::KeyCode key, Event*)
        {
            auto gameScene = GameScene::createScene();
            ArrowPool::getInstance().initialize(100, gameScene);
            auto transition = TransitionFade::create(_transitionTime, gameScene);
            Director::getInstance()->replaceScene(transition);
        };

    _eventDispatcher->addEventListenerWithSceneGraphPriority(keyboardEvent, this);
}

