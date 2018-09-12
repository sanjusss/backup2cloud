
[![Docker Automated build](https://img.shields.io/docker/automated/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![Docker Build Status](https://img.shields.io/docker/build/sanjusss/backup2cloud.svg)](https://hub.docker.com/r/sanjusss/backup2cloud)
[![Build status](https://ci.appveyor.com/api/projects/status/9sa1mtm53jxket5t?svg=true)](https://ci.appveyor.com/project/sanjusss/backup2cloud)
[![GitHub release](https://img.shields.io/github/release/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/releases)  
[![GitHub tag](https://img.shields.io/github/tag/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/tags)
[![GitHub issues](https://img.shields.io/github/issues/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/issues)
[![GitHub forks](https://img.shields.io/github/forks/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/network)
[![GitHub stars](https://img.shields.io/github/stars/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/stargazers)
[![GitHub license](https://img.shields.io/github/license/sanjusss/backup2cloud.svg)](https://github.com/sanjusss/backup2cloud/blob/master/LICENSE)

# 用途
定时将服务器上的文件或文件夹备份到网络云存储（阿里云oss或腾讯云cos）。  
可以设置在多个时间点，备份多个文件或文件夹到多个网络云存储。  

# 支持的云存储
[阿里云OSS](https://www.aliyun.com/product/oss)、
[腾讯云COS](https://cloud.tencent.com/product/cos)、
[百度云BOS](https://cloud.baidu.com/product/bos.html)、
[七牛云对象存储](https://www.qiniu.com/products/kodo)、
[AWS S3](https://amazonaws-china.com/cn/s3/)、
[UCloud对象存储（UFile）](https://www.ucloud.cn/site/product/ufile.html)、
[华为云OBS](https://www.huaweicloud.com/product/obs.html) 

# 使用方法(Docker)
### 运行
```
docker run -d --restart=always -v /etc/localtime:/etc/localtime:ro -v /xxx/yyy:/data -v /etc/backup2cloud:/conf sanjusss/backup2cloud run -c /conf/backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。  
`-v /etc/localtime:/etc/localtime:ro`保证了docker容器内时区和外部环境一样，否则默认使用UTC时间。  
注意文件夹的挂载。  

### 查看示例配置文件
```
docker run --rm -v /D/share:/conf sanjusss/backup2cloud eg -s /conf/eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s /conf/eg.json`，将不保存文件，仅在命令行下输出。  
示例文件 [点我](https://github.com/sanjusss/backup2cloud/blob/master/example.json)  
实际的配置文件中没有Tips一项。  

### 查看命令帮助
```
docker run --rm sanjusss/backup2cloud --help
```

### 查看软件版本
```
docker run --rm sanjusss/backup2cloud --version
```


# 使用方法(Windows)
### 下载
[点我打开软件下载页面](https://github.com/sanjusss/backup2cloud/releases/latest)  

### 运行
```
backup2cloud run -c D:\backup2cloud.json
```
运行后程序将加载配置文件backup2cloud.json。

### 查看示例配置文件
```
backup2cloud eg -s D:\share\eg.json
```
运行后将在D:\share\eg.json生成示例配置文件。  
如果省略`-s D:\share\eg.json`，将不保存文件，仅在命令行下输出。  
示例文件 [点我](https://github.com/sanjusss/backup2cloud/blob/master/example.json)  
实际的配置文件中没有Tips一项。  

### 查看命令帮助
```
backup2cloud --help
```

### 查看软件版本
```
backup2cloud --version
```


# 引用项目
https://www.newtonsoft.com/json  
https://github.com/vla/aliyun-oss-csharp-sdk  
https://github.com/commandlineparser/commandline  
https://github.com/zhengchun/qcloud-sdk-net  
https://github.com/adamhathcock/sharpcompress  
https://www.quartz-scheduler.net  
https://github.com/sanjusss/backup2cloud  
https://github.com/fengyhack/csharp-sdk  
https://github.com/sanjusss/ucloud-csharp-sdk  
https://github.com/aws/aws-sdk-net/  
https://github.com/awslabs/aws-sdk-net-samples  
https://support.huaweicloud.com/devg-obs_csharp_sdk_doc_zh/zh-cn_topic_0120606335.html  
