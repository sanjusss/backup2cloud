
[![Docker Automated build](https://img.shields.io/docker/automated/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![Docker Build Status](https://img.shields.io/docker/build/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![GitHub issues](https://img.shields.io/github/issues/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/issues)
[![GitHub forks](https://img.shields.io/github/forks/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/network)
[![GitHub stars](https://img.shields.io/github/stars/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/stargazers)
[![GitHub license](https://img.shields.io/github/license/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/blob/master/LICENSE)

# 用途
定时将服务器上的文件或文件夹备份到网络云存储（阿里云oss或腾讯云cos）。  
可以设置在多个时间点，备份多个文件或文件夹到多个网络云存储。  

# 支持的云存储
[阿里云OSS](https://www.aliyun.com/product/oss)  
[腾讯云COS](https://cloud.tencent.com/product/cos)  
[百度云BOS](https://cloud.baidu.com/product/bos.html)  
[七牛云对象存储](https://www.qiniu.com/products/kodo)  

# 使用方法(Docker)
## 运行
```
docker run -d --restart=always -v /etc/localtime:/etc/localtime:ro -v /xxx/yyy:/data -v /etc/backup2cloud:/conf sanjusss/backup2cloud run -c /conf/backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。  
`-v /etc/localtime:/etc/localtime:ro`保证了docker容器内时区和外部环境一样，否则默认使用UTC时间。  
注意文件夹的挂载。  

## 查看示例配置文件
```
docker run --rm -v /D/share:/conf sanjusss/backup2cloud eg -s /conf/eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s /conf/eg.json`，将不保存文件，仅在命令行下输出。  
示例文件 [点我](https://github.com/sanjusss/backup2cloud/blob/master/example.json)  
实际的配置文件中没有Tips一项。  

## 查看命令帮助
```
docker run --rm sanjusss/backup2cloud --help
```

## 查看软件版本
```
docker run --rm sanjusss/backup2cloud --version
```

# 引用项目
https://www.newtonsoft.com/json  
https://github.com/vla/aliyun-oss-csharp-sdk  
https://github.com/commandlineparser/commandline  
https://github.com/zhengchun/qcloud-sdk-net  
https://github.com/adamhathcock/sharpcompress  
https://www.quartz-scheduler.net  
https://github.com/sanjusss/bce-sdk-dotnet  
